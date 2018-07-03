// This file is part of the Unity-MVC project
// https://github.com/RLefrancoise/Unity-MVC
// 
// BSD 3-Clause License
// 
// Copyright (c) 2018, Renaud Lefrancoise
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
// 
// * Redistributions of source code must retain the above copyright notice, this
//   list of conditions and the following disclaimer.
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution.
// 
// * Neither the name of the copyright holder nor the names of its
//   contributors may be used to endorse or promote products derived from
//   this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System.Collections;
using UnityEngine;

namespace Other
{
    public static class TransformExtensions
    {
        public static void Move(this Transform transform, MonoBehaviour monoBehaviour, Vector3 source, Vector3 dest, float moveTime = 1f)
        {
            monoBehaviour.StartCoroutine(_MoveTransform(transform, source, dest, moveTime));
        }

        public static IEnumerator Move(this Transform transform, Vector3 start, Vector3 end, float animTime = 1f)
        {
            yield return _MoveTransform(transform, start, end, animTime);
        }

        private static IEnumerator _MoveTransform(Transform target, Vector3 start, Vector3 end, float animTime = 1f)
        {
            float t = 0f;
            while (t <= 1f)
            {
                target.GetComponent<RectTransform>().localPosition = Vector3.Lerp(start, end, t);
                t += Time.deltaTime / animTime;
                if (t > 1f && target.GetComponent<RectTransform>().localPosition != end) t = 1f;
                yield return null;
            }
        }
    }
}
