using UnityEngine;
using LLMUnity;
using UnityEngine.UI;

namespace LLMUnitySamples
{
    public class SimpleInteraction : MonoBehaviour
    {
        public LLM llm;
        public TextToSpeech tts;

        public InputField playerText;
        public Text AIText;


        private bool IsGenerating;
        private float startTime;

        void Start()
        {
            playerText.onSubmit.AddListener(onInputFieldSubmit);
            playerText.Select();
        }

        void onInputFieldSubmit(string message)
        {
            IsGenerating = true;
            playerText.interactable = false;
            AIText.text = "...";

            startTime = Time.time;
            _ = llm.Chat(message, SetAIText, AIReplyComplete);
        }

        public void SetAIText(string text)
        {
            AIText.text = text;
            // Debug.Log(text);

            tts.StartGeneration(text, startTime);
        }

        public void AIReplyComplete()
        {
            playerText.interactable = true;
            playerText.Select();
            playerText.text = "";
        }

        public void CancelRequests()
        {
            llm.CancelRequests();
            AIReplyComplete();
        }

        public void ExitGame()
        {
            Debug.Log("Exit button clicked");
            Application.Quit();
        }
    }
}
