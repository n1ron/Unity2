using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Geekbrains
{
	public class BotController : BaseController, IOnUpdate, IInitialization
	{
		public int CountBot = 0;
		public HashSet<Bot> GetBotList { get; } = new HashSet<Bot>();

		public void OnStart()
		{
			for (var index = 0; index < CountBot; index++)
			{
				var tempBot = Object.Instantiate(Main.Instance.RefBotPrefab,
					Patrol.GenericPoint(Main.Instance.Player),
					Quaternion.identity);

				tempBot.Agent.avoidancePriority = index;
				tempBot.Target = Main.Instance.Player; //todo разных противников
				AddBotToList(tempBot);
                tempBot.OnDieChange += RemoveBotToList;
            }
		}

		private void AddBotToList(Bot bot)
		{
			if (!GetBotList.Contains(bot))
			{
				GetBotList.Add(bot);
			}
		}

        private void RemoveBotToList(Bot bot)
		{
			if (GetBotList.Contains(bot))
			{
                bot.OnDieChange -= RemoveBotToList;
				GetBotList.Remove(bot);
            }
		}

		public void OnUpdate()
        {
            if (!IsActive) return;
            for (var i = 0; i < GetBotList.Count; i++)
            {
                var bot = GetBotList.ElementAt(i);
                bot.Tick();
            }
        }
	}
}