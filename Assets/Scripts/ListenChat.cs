using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TwitchIRC))]
public class ListenChat : MonoBehaviour {

	private TwitchIRC IRC;

	// Use this for initialization
	void Start()
	{
		IRC = this.GetComponent<TwitchIRC>();
		//IRC.SendCommand("CAP REQ :twitch.tv/tags"); //register for additional data such as emote-ids, name color etc.
		IRC.messageRecievedEvent.AddListener(OnChatMsgRecieved);
	}

	//when message is recieved from IRC-server or our own message.
	void OnChatMsgRecieved(string msg)
	{
		//parse from buffer.
		int msgIndex = msg.IndexOf("PRIVMSG #");
		string msgString = msg.Substring(msgIndex + IRC.channelName.Length + 11);
		string user = msg.Substring(1, msg.IndexOf('!') - 1);

		if (msgString.Contains ("!test")) {
			Debug.Log ("User: " + user + " did a test.");
		}
	}
}
