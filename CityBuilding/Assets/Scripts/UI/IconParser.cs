using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconParser : MonoBehaviour
{
    public static IconParser instance { get; private set; }

    private void Awake()
    {
      instance = this;
    }

    public string Parse(string Text)
    {
      Text = Text.Replace("Water", " <sprite name=\"Water\"/>");
      Text = Text.Replace("Iron", " <sprite name=\"Iron\"/>");
      Text = Text.Replace("Energy", " <sprite name=\"Energy\"/>");
      Text = Text.Replace("Money", " <sprite name=\"Money\"/>");

      return Text;
    }
}
