using Agate.MVC.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace ProjectTA.Module.GoogleFormUploader
{
    public class GoogleFormUploaderView : BaseView
    {
        public void SubmitForm(string formUrl, Dictionary<string, string> keyValues, UnityAction<long> callback = null)
        {
            StartCoroutine(SubmitFormRoutine(formUrl, keyValues, callback));
        }

        private IEnumerator SubmitFormRoutine(string formUrl, Dictionary<string, string> keyValues, UnityAction<long> callback)
        {
            WWWForm form = new();

            foreach (var keyValue in keyValues)
            {
                Debug.Log($"{keyValue.Key}: {keyValue.Value}");
                form.AddField(keyValue.Key, keyValue.Value);
            }

            UnityWebRequest request = UnityWebRequest.Post(formUrl, form);
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Form submitted successfully!");
            }
            else
            {
                Debug.LogError($"Error submitting form: {request.error}");
            }

            callback?.Invoke(request.responseCode);
        }
    }
}