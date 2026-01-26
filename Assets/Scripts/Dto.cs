using System;

[Serializable]
public class GameStateDto
{
    public GameV2Dto game;
    public PromptDto prompt;
    public PlayerV2Dto[] players;
}

[Serializable]
public class PlayerV2Dto
{
    public int id;
    public string name;
    public string model;
    public string role;
    public string voice;
    public string mood;
    public string ttsModel;
    public float voiceTemperature;
    public string voiceStyle;
    public int position;
    public int gameId;
}


[Serializable]
public class GameV2Dto
{
    public int id;
    public int missionPriority;
    public int leaderPosition;
    public int speakerPosition;
    public int skipsCount;
    public int wins;
    public int fails;
    public string gameState;
}

[Serializable]
public class PromptDto
{
    public int id;
    public int gameId;
    public string model;
    public string systemPrompt;
    public string messagePrompt;
    public string response;
    public string status;
}