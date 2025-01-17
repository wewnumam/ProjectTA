using Agate.MVC.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ProjectTA.Module.GoogleFormUploader
{
    public class GoogleFormUploaderView : BaseView
    {
        public void SubmitForm(string formUrl, Dictionary<string, string> keyValues)
        {
            StartCoroutine(SubmitFormRoutine(formUrl, keyValues));
        }

        private IEnumerator SubmitFormRoutine(string formUrl, Dictionary<string, string> keyValues)
        {
            WWWForm form = new();

            foreach (var keyValue in keyValues)
            {
                form.AddField(keyValue.Key, keyValue.Value);
            }

            UnityWebRequest www = UnityWebRequest.Post(formUrl, form);
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Form submitted successfully!");
            }
            else
            {
                Debug.LogError($"Error submitting form: {www.error}");
            }
        }
    }
}