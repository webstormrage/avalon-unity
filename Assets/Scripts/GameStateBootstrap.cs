using UnityEngine;

public class GameStateBootstrap : MonoBehaviour
{
    void Start()
    {
        var initialState = new GameStateDto
        {
            game = new GameV2Dto
            {
                id = 4,
                wins = 2,
                fails = 2,
                skipsCount = 0,
                leaderPosition = 4,
                speakerPosition = 3,
                missionPriority = 5,
                gameState = "MISSION"
            },
            prompt = new PromptDto
            {
                id = 51,
                gameId = 4,
                model = "models/gemini-2.5-flash",
                systemPrompt = "",
                messagePrompt = "",
                response = @"
                Ах, ну вот и настал момент истины, не так ли? Триумф Синих, как они его называют, но ведь это лишь прелюдия к настоящей игре. Я внимательно следила за каждым словом, за каждым движением. И знаете, кто здесь самый... *проницательный*? Тот, кто пытается казаться самым честным, самым преданным, но при этом делает самые рискованные ходы.

Вторая миссия. Лидер, который с таким пафосом говорил о ""непоколебимом духе Камелота"", и при этом собрал команду, которая *гарантированно* провалилась. Это был не промах, нет. Это была *провокация*. Попытка выставить Красных, прикрываясь громкими словами. И это сработало, не так ли? Мы, Красные, были вынуждены показать себя, чтобы игра продолжилась.

А потом этот же игрок, после такого ""провала"", вдруг оказывается в успешных командах, снова вещая о ""доверии"" и ""верности"". Слишком уж гладко, слишком уж... *удобно*.

Сэр Бранн, вы слишком прямолинейны. Сэр Кайлен, вы слишком... податливы. Но Сэр Лиорен? Он играет в свою собственную игру, пытаясь манипулировать всеми, прикрываясь маской добродетели. Он тот, кто видит нас, Красных, и пытается использовать это знание.

Выбрать: Сэр Лиорен
                ",
                status = "HAS_RESPONSE"
            },
            players = new PlayerV2Dto[]
            {
                new PlayerV2Dto{
                    id = 1,
                    name = "Петир",
                    role = "Слуга Артура",
                    position = 1,
                    voice = "Aoede",
                    voiceTemperature = 0.8f,
                    voiceStyle = "",
                    mood = "",
                    model = "models/gemini-2.5-flash",
                    ttsModel = "gemini-2.5-flash-preview-tts",
                    gameId = 4
                },
                new PlayerV2Dto{
                    id = 2,
                    name = "Варис",
                    role = "Мерлин",
                    position = 2,
                    voice = "Charon",
                    voiceTemperature = 0.4f,
                    voiceStyle = "",
                    mood = "",
                    model = "models/gemini-2.5-flash",
                    ttsModel = "gemini-2.5-flash-preview-tts",
                    gameId = 4
                },
                new PlayerV2Dto{
                    id = 3,
                    name = "Серсея",
                    role = "Ассасин",
                    position = 3,
                    voice = "Kore",
                    voiceTemperature = 0.3f,
                    voiceStyle = "",
                    mood = "",
                    model = "models/gemini-2.5-flash",
                    ttsModel = "gemini-2.5-flash-preview-tts",
                    gameId = 4
                },
                new PlayerV2Dto{
                    id = 4,
                    name = "Тирион",
                    role = "Миньон Мордреда",
                    position = 4,
                    voice = "Fenrir",
                    voiceTemperature = 0.9f,
                    voiceStyle = "",
                    mood = "",
                    model = "models/gemini-2.5-flash",
                    ttsModel = "gemini-2.5-flash-preview-tts",
                    gameId = 4
                },
                new PlayerV2Dto{
                    id = 5,
                    name = "Барристан",
                    role = "Слуга Артура",
                    position = 5,
                    voice = "Puck",
                    voiceTemperature = 0.2f,
                    voiceStyle = "",
                    mood = "",
                    model = "models/gemini-2.5-flash",
                    ttsModel = "gemini-2.5-flash-preview-tts",
                    gameId = 4
                }
            }
        };

        GameStateManager.Instance.SetState(initialState);
    }
}
