using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscores : MonoBehaviour
{
	const string privateCode = "Q_8EaqhcUEiIIKZLUTYTAwcqut3nmHdEacHFcb1jqvfg";
	const string publicCode = "61ffdaaa8f40bb1058cc02e7";
	const string webURL = "http://dreamlo.com/lb/";

	DisplayHighscores highscoreDisplay;
	public Highscore[] highscoresList;
	static Highscores instance;

	void Awake()
	{
		highscoreDisplay = GetComponent<DisplayHighscores>();
		instance = this;
		AddNewHighscore(username:("fhex"), 0, 990);
		AddNewHighscore(username: ("fhexx"), 0, 890);
		AddNewHighscore(username: ("fhexxx"), 0, 790);
	}

	public static void AddNewHighscore(string username, int score, int time)
	{
		instance.StartCoroutine(instance.UploadNewHighscore(username, score, time));
	}

	IEnumerator UploadNewHighscore(string username, int score, int time)
	{
		WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + time);
		yield return www;

		if (string.IsNullOrEmpty(www.error))
		{
			print("Upload Successful");
			DownloadHighscores();
		}
		else
		{
			print("Error uploading: " + www.error);
		}
	}

	public void DownloadHighscores()
	{
		StartCoroutine("DownloadHighscoresFromDatabase");
	}

	IEnumerator DownloadHighscoresFromDatabase()
	{
		WWW www = new WWW(webURL + publicCode + "/pipe/");
		yield return www;

		if (string.IsNullOrEmpty(www.error))
		{
			FormatHighscores(www.text);
			highscoreDisplay.OnHighscoresDownloaded(highscoresList);
		}
		else
		{
			print("Error Downloading: " + www.error);
		}
	}

	void FormatHighscores(string textStream)
	{
		string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
		highscoresList = new Highscore[entries.Length];

		for (int i = 0; i < entries.Length; i++)
		{
			string[] entryInfo = entries[i].Split(new char[] { '|' });
			string username = entryInfo[0];
			int score = int.Parse(entryInfo[1]);
			int time = int.Parse(entryInfo[2]);
			highscoresList[i] = new Highscore(username, score, time);
			print(highscoresList[i].username + ": " + highscoresList[i].score);
		}
	}

}

public struct Highscore
{
	public string username;
	public int score;
	public int time;

	public Highscore(string _username, int _score, int _time)
	{
		username = _username;
		score = _score;
		time = _time;
	}

}