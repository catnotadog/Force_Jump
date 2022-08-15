using System.Collections;
using MelonLoader;
using UnityEngine;

namespace Jump
{
    class Main : MelonMod
    {

        public override void OnApplicationStart()
        {
            MelonCoroutines.Start(waitforui());
        }
     
                
        private IEnumerator waitforui()
        {
            MelonLogger.Msg("Waiting For Ui");
            while (GameObject.Find("UserInterface") == null)
                yield return null;

            while (GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)") == null)
                yield return null;

            MelonLogger.Msg("Ui loaded");

            var toinstjumpmod = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emotes");
            var instjumpmod = GameObject.Instantiate(toinst, toinst.parent).gameObject;
            instjumpmod.name = "Button jump";
            var txtjumpmod = instjumpmod.transform.Find("Container/Text_QM_H3").GetComponent<TMPro.TextMeshProUGUI>();
            txtjumpmod.richText = true;
            txtjumpmod.text = $"<color=#000080ff>Jump</color>";
            GameObject.DestroyImmediate(instjumpmod.transform.Find("Container/Icon").gameObject);
            var btnjumpmod = instjumpmod.GetComponent<UnityEngine.UI.Button>();
            btnjumpmod.onClick.RemoveAllListeners();
            btnjumpmod.onClick.AddListener(new System.Action(() => { VRC.SDKBase.Networking.LocalPlayer.SetJumpImpulse(4); VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true; }));

            

        }
        
        public override void OnUpdate()
        {
            if (true) return;

            {
                VRC.SDKBase.Networking.LocalPlayer.SetJumpImpulse(4);
            }

        }

    }
}
