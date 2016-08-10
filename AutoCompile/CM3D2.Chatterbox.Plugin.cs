using System;
using System.Collections.Generic;
using UnityEngine;
using UnityInjector;
using UnityInjector.Attributes;

namespace CM3D2.Chatterbox
{
	[PluginFilter("CM3D2x64"), PluginFilter("CM3D2x86"), PluginName("Chatterbox"), PluginVersion("0.0.0.1")]
	public class Chatterbox : PluginBase
	{
		public bool enable = false;
		public Dictionary<param.Personal, string> prefix = new Dictionary<param.Personal, string>()
		{
			{ param.Personal.Pure, "s2_" },
			{ param.Personal.Cool, "s1_" },
			{ param.Personal.Pride, "s0_" },
			{ param.Personal.Yandere, "s3_" },
			{ param.Personal.Anesan, "s4_" },
		};

		public void Awake()
		{
			UnityEngine.Object.DontDestroyOnLoad(this);
		}

		private void OnLevelWasLoaded(int level)
		{
			enable = level == 27;
		}

		public void Update()
		{
			if (GameMain.Instance.CharacterMgr.IsBusy() || !enable) return;

			for (int i = 0; i < GameMain.Instance.CharacterMgr.GetMaidCount() - 1; ++i)
			{
				var md = GameMain.Instance.CharacterMgr.GetMaid(i);
				if (md == null || md.AudioMan == null) continue;
				if (!md.AudioMan.isPlay())
				{
					var n = UnityEngine.Random.Range(0, 9999);
					var s = prefix[md.Param.status.personal];
					md.AudioMan.LoadPlay(s + String.Format("{0:00000}", n) + ".ogg", 0.0f, false, false);
				}
			}
		}
	}
}