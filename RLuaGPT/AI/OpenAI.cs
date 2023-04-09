using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace RLuaGPT
{
    internal class OpenAI
    {
        public static async void Ask(string input, RichTextBox richTextBox1, RichTextBox richTextBox2)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (!string.IsNullOrEmpty(input))
                    {
                        client.DefaultRequestHeaders.Add("authorization", $"Bearer { Base.key }");
                        Base.Root chatGPT = new()
                        {
                            model = "gpt-3.5-turbo",
                            temperature = (int)0.2,
                            max_tokens = 1000,
                            messages = new List<Base.Message>()
                        {
                            new Base.Message() { role = "system", content = Base.script },
                            new Base.Message() { role = "user", content = input }}
                        };
                        string response = await (await client.PostAsync(Base.endpoint, new StringContent(JsonConvert.SerializeObject(chatGPT), Encoding.UTF8, "application/json"))).Content.ReadAsStringAsync();

                        richTextBox1.Text = JsonConvert.DeserializeObject<dynamic>(response).choices[0].message.content;
                        richTextBox2.Text = response;
                    }
                }
                catch (Exception exception)
                { MessageBox.Show(exception.ToString(), "RLua GPT", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}