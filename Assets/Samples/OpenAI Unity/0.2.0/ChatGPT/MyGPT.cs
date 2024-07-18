using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace OpenAI
{
    public class MyGPT : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private Text textArea;


        private OpenAIApi openai = new OpenAIApi();

        private string userInput;
        private string prompt = "Act as a random stranger in a chat room and reply to the questions.\nQ: ";

        private void Start()
        {
            button.onClick.AddListener(SendReply);
        }


        private async void SendReply()
        {
            userInput = inputField.text;
            prompt += $"{userInput}\nA: ";


            textArea.text = "...";
            inputField.text = "";

            button.enabled = false;
            inputField.enabled = false;

            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                //Prompt = prompt,
                Model = "text-davinci-003",
                MaxTokens = 128
            });

            button.enabled = true;
            inputField.enabled = true;
        }
    }
}
