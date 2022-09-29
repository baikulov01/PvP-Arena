﻿/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * This source code is licensed under the license found in the
 * LICENSE file in the root directory of this source tree.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Meta.Conduit
{
    /// <summary>
    /// The manifest is the core artifact generated by Conduit that contains the relevant information about the app.
    /// This information can be used to train the backend or dispatch incoming requests to methods.
    /// </summary>
    internal class Manifest
    {
        /// <summary>
        /// Called via JSON reflection, need preserver or it will be stripped on compile
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public Manifest() { }

        /// <summary>
        /// The App ID.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// The version of the Manifest format.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// A human friendly name for the application/domain.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// List of relevant entities.
        /// </summary>
        public List<ManifestEntity> Entities { get; set; } = new List<ManifestEntity>();

        /// <summary>
        /// List of relevant actions (methods).
        /// </summary>
        public List<ManifestAction> Actions { get; set; } = new List<ManifestAction>();

        /// <summary>
        /// Maps action IDs (intents) to CLR methods. Each entry in the value list is a different overload of the method.
        /// The list is sorted with the most parameters listed first, so we get maximal matches during dispatching by
        /// default without needing to sort them at runtime.
        /// </summary>
        private readonly Dictionary<string, List<InvocationContext>> methodLookup =
            new Dictionary<string, List<InvocationContext>>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Processes all actions in the manifest and associate them with the methods they should invoke.
        /// </summary>
        public bool ResolveActions()
        {
            var resolvedAll = true;
            foreach (var action in this.Actions)
            {
                var lastPeriod = action.ID.LastIndexOf('.');
                if (lastPeriod <= 0)
                {
                    Debug.LogError($"Invalid Action ID: {action.ID}");
                    resolvedAll = false;
                    continue;
                }

                var typeName = action.ID.Substring(0, lastPeriod);
                var qualifiedTypeName = $"{typeName},{action.Assembly}";
                var method = action.ID.Substring(lastPeriod + 1);

                var targetType = Type.GetType(qualifiedTypeName);
                if (targetType == null)
                {
                    Debug.LogError($"Failed to resolve type: {qualifiedTypeName}");
                    resolvedAll = false;
                    continue;
                }

                var types = new Type[action.Parameters.Count];
                for (var i = 0; i < action.Parameters.Count; i++)
                {
                    var manifestParameter = action.Parameters[i];
                    var fullTypeName = $"{manifestParameter.QualifiedTypeName},{manifestParameter.TypeAssembly}";
                    types[i] = Type.GetType(fullTypeName);
                }

                var targetMethod = targetType.GetMethod(method,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static, null, CallingConventions.Any,
                    types, null);
                if (targetMethod == null)
                {
                    Debug.LogError($"Failed to resolve method {method}.");
                    resolvedAll = false;
                    continue;
                }

                var attributes = targetMethod.GetCustomAttributes(typeof(ConduitActionAttribute), false);
                if (attributes.Length == 0)
                {
                    Debug.LogError($"{targetMethod} - Did not have expected Conduit attribute");
                    resolvedAll = false;
                    continue;
                }
                var actionAttribute = attributes.First() as ConduitActionAttribute;

                var invocationContext = new InvocationContext()
                {
                    Type = targetType,
                    MethodInfo = targetMethod,
                    MinConfidence = actionAttribute.MinConfidence,
                    MaxConfidence = actionAttribute.MaxConfidence
                };

                if (!this.methodLookup.ContainsKey(action.Name))
                {
                    this.methodLookup.Add(action.Name, new List<InvocationContext>());
                }

                this.methodLookup[action.Name].Add(invocationContext);
            }

            foreach (var invocationContext in this.methodLookup.Values.Where(invocationContext =>
                         invocationContext.Count > 1))
            {
                // This is a slow operation. If there multiple overloads are common, we should optimize this
                invocationContext.Sort((one, two) =>
                    two.MethodInfo.GetParameters().Length - one.MethodInfo.GetParameters().Length);
            }

            return resolvedAll;
        }

        /// <summary>
        /// Returns true if the manifest contains the specified action.
        /// </summary>
        /// <param name="action"></param>
        /// <returns>True if the action exists, false otherwise.</returns>
        public bool ContainsAction(string @action)
        {
            return this.methodLookup.ContainsKey(action);
        }

        /// <summary>
        /// Returns the invocation context for the specified action ID.
        /// </summary>
        /// <param name="actionId">The action ID.</param>
        /// <returns>The invocationContext.</returns>
        public List<InvocationContext> GetInvocationContexts(string actionId)
        {
            return this.methodLookup[actionId];
        }
    }
}
