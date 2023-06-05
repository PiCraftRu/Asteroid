using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroid.UI
{
    public class ScriptableButton : MonoBehaviour
    {
        [SerializeField] string[] _commands;
        [SerializeField] GameObject _prefabAsteroid;

        private class CommandLine
        {
            public string command;
            public string[] arguments;
        }

        private CommandLine Parse(string command)
        {
            var words = command.Split(' ');
            if (command.Length > 0 )
            {
                var line = new CommandLine();
                line.command = words[0];
                line.arguments = new string[words.Length - 1];
                Array.Copy(words, 1, line.arguments, 0, words.Length - 1);
                return line;
            }
            return null;
        }

        private void Execute(string command)
        {
            var line = Parse(command);
            if (line != null)
            {
                if (line.command == "restart")
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else if (line.command == "url" && line.arguments.Length == 1)
                {
                    Application.OpenURL(line.arguments[0]);
                }
                else if (line.command == "destroy" && line.arguments.Length == 1)
                {
                    var go = GameObject.Find(line.arguments[0]);
                    if (go != null)
                    {
                        Destroy(go);
                    }
                }
                else if (line.command == "asteroid" && line.arguments.Length == 0)
                {
                    Instantiate(_prefabAsteroid, new Vector3(2, 0, -5), Quaternion.identity);
                }
            }
        }

        public void Click()
        {
            foreach (var command in _commands)
            {
                Execute(command);
            }
        }
    }
}
