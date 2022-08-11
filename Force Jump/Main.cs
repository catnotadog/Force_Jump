using System.Collections;
using MelonLoader;
using UnityEngine;

namespace Jump
{
    class Main : MelonMod
    {
        private bool jumptoggle = false;

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

            var toinst = GameObject.Find("/UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emotes");
            var inst = GameObject.Instantiate(toinst, toinst.parent).gameObject;
            var txt = inst.transform.Find("Container/Text_QM_H3").GetComponent<TMPro.TextMeshProUGUI>();
            txt.richText = true;
            txt.text = $"<color=#000080ff>Jump</color>";
            GameObject.DestroyImmediate(inst.transform.Find("Container/Icon").gameObject);
            var btn = inst.GetComponent<UnityEngine.UI.Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(new System.Action(() => { jumptoggle = !jumptoggle; _ = jumptoggle ? txt.text = $"<color=#ff0000ff>Jump</color>" : txt.text = $"<color=#000080ff>Jump</color>"; VRC.Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = !jumptoggle; }));

            

        }
        
        public override void OnUpdate()
        {
            if (!jumptoggle) return;

            {
                VRC.SDKBase.Networking.LocalPlayer.SetJumpImpulse(4);
            }

        }

    }
}
