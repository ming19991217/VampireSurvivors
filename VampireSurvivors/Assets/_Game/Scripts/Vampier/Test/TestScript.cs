using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        var ui = new UI()
                .SetView("MainView")
                .SetText("Hello World")
                .SetDescription("This is a test description");


        List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        for (int i = 0, len = numbers.Count; i < len; i++)
        {
            Debug.Log("Hello World");
        }
    }




    class UI
    {



        public UI SetView(string viewName)
        {

            return this;
        }

        public UI SetDescription(string description)
        {
            return this;
        }

        public UI SetViewUI(string viewName)
        {
            return this;
        }


        public UI SetText(string text)
        {
            return this;
        }


    }
}
