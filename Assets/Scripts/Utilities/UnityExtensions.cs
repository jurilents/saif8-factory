using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Utilities
{
    #region Public

    /// <summary>
    /// Putting these in the global namespace so that they can be accessed from anywhere in the game without importing
    /// </summary>
    public static class UnityExtensions
    {
        /// <summary>
        /// A shortcut for creating a new game object then adding a component then adding it to a parent object
        /// </summary>
        /// <typeparam name="T">Type of component</typeparam>
        /// <returns>The new component</returns>
        public static T AddChild<T>(this GameObject parent) where T : Component
        {
            return AddChild<T>(parent, typeof(T).Name);
        }

        /// <summary>
        /// A shortcut for adding a given game object as a child
        /// </summary>
        /// <returns>This gameobject</returns>
        public static GameObject AddChild(this GameObject parent, GameObject child, bool worldPositionStays = false)
        {
            child.transform.SetParent(parent.transform, worldPositionStays);
            return parent;
        }

        /// <summary>
        /// A shortcut for creating a new game object then adding a component then adding it to a parent object
        /// </summary>
        /// <typeparam name="T">Type of component</typeparam>
        /// <param name="parent">Instance of the GameObject class</param>
        /// <param name="name">Name of the child game object</param>
        /// <returns>The new component</returns>
        public static T AddChild<T>(this GameObject parent, string name) where T : Component
        {
            GameObject obj = AddChild(parent, name, typeof(T));
            return obj.GetComponent<T>();
        }

        /// <summary>
        /// A shortcut for creating a new game object with a number of components and adding it as a child
        /// </summary>
        /// <param name="parent">Instance of the GameObject class</param>
        /// <param name="components">A list of component types</param>
        /// <returns>The new game object</returns>
        public static GameObject AddChild(this GameObject parent, params Type[] components)
        {
            return AddChild(parent, "Game Object", components);
        }

        /// <summary>
        /// A shortcut for creating a new game object with a number of components and adding it as a child
        /// </summary>
        /// <param name="parent">Instance of the GameObject class</param>
        /// <param name="name">The name of the new game object</param>
        /// <param name="components">A list of component types</param>
        /// <returns>The new game object</returns>
        public static GameObject AddChild(this GameObject parent, string name, params Type[] components)
        {
            var obj = new GameObject(name, components);
            if (parent is null) return obj;
            if (obj.transform is RectTransform) obj.transform.SetParent(parent.transform, true);
            else obj.transform.parent = parent.transform;

            return obj;
        }


        /// <summary>
        /// Destroys all the children of a given transform
        /// </summary>
        /// <param name="transform">The parent transform</param>
        public static void DestroyAllChildrenImmediately(this Transform transform)
        {
            while (transform.childCount != 0)
                Object.DestroyImmediate(transform.GetChild(0).gameObject);
        }


        /// <summary>
        /// Focuses the camera on a point in 2D space (just transforms the x and y to match the target)
        /// </summary>
        public static void FocusOn2D(this Camera camera, GameObject target)
        {
            Vector3 localPos = target.transform.localPosition;
            Transform cameraTransform = camera.transform;
            cameraTransform.position = new Vector3(localPos.x, localPos.y, cameraTransform.position.z);
        }

        /// <summary>
        /// Focuses the camera on a point in 2D space (just transforms the x and y to match the target)
        /// </summary>
        public static void FocusOn2D(this Camera camera, GameObject target, Camera mainCamera)
        {
            Vector3 localPos = target.transform.localPosition;
            camera.transform.position = new Vector3(localPos.x, localPos.y, mainCamera.transform.position.z);
        }


        /// <summary>
        /// Converts a timespan to a readable format but in a shorter form
        /// </summary>
        public static string ToReadableString(this TimeSpan t)
        {
            TimeSpan duration = t.Duration();
            string d = FormatTimeString(duration.Days, t.Days);
            string h = FormatTimeString(duration.Hours, t.Hours);
            string m = FormatTimeString(duration.Minutes, t.Minutes);
            string s = FormatTimeString(duration.Seconds, t.Seconds);
            string ms = FormatTimeString(duration.Milliseconds, t.Milliseconds);

            string formatted = $"{d}:{h}:{m}:{s}:{ms}".Trim(':', ' ');
            return string.IsNullOrEmpty(formatted) ? "0 ms" : formatted;
        }


        /// <summary>
        /// Simple method to turn a v2 into a v3
        /// </summary>
        /// <param name="v">The vector to convert</param>
        /// <returns></returns>
        public static Vector3 ToVector3(this Vector2 v)
        {
            return new Vector3(v.x, v.y, 0);
        }

        /// <summary>
        /// Simple method to turn a v3 into a v2
        /// </summary>
        /// <param name="v">The vector to convert</param>
        /// <returns></returns>
        public static Vector2 ToVector2(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }


        public static void Rotate(this ref Vector3 vector, int angle)
        {
            vector = Quaternion.Euler(0, 0, angle) * vector;
        }


        /// <summary>
        /// Quick shuffle of a list 
        /// Borrowed from: http://stackoverflow.com/questions/273313/randomize-a-listt-in-c-sharp
        /// </summary>
        /// <typeparam name="T">the type of the list</typeparam>
        /// <param name="list">the list to shuffle</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }


        /// <summary>
        /// NOT TESTED !!
        /// Converts a 3D bounds to a 2D rect
        /// </summary>
        /// <param name="b">The bounds to convert</param>
        /// <returns>the rect</returns>
        public static Rect ToRect(this Bounds b)
        {
            return new Rect(b.min.x, b.min.y, b.size.x, b.size.y);
        }

        #endregion


        #region Private

        private static string FormatTimeString(int duration, int time) => duration > 0 ? $"{time:00}" : "";

        #endregion
    }
}