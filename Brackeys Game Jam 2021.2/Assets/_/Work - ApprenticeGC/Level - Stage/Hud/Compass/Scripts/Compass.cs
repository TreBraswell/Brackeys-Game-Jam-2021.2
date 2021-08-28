namespace BGJ20212.Game.ApprenticeGC.Hud
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Bolt;
    using UnityEngine;
    using UnityEngine.Timeline;
    using UnityEngine.UI;

    public class Compass : MonoBehaviour
    {
        public GameObject iconPrefab;
        public RawImage compassImage;

        public float maxDistance;

        private GameObject _player;

        // private List<UIIndication> _indications = new List<UIIndication>();
        private readonly Dictionary<UIIndication, GameObject> _keyedIndications = new Dictionary<UIIndication, GameObject>();

        private float _compassUnit;

        private void Start()
        {
            //
            _compassUnit = compassImage.rectTransform.rect.width / 360.0f;

            //
            // var rank1Manager = GameObject.FindGameObjectWithTag("Rank1Manager");
            // if (rank1Manager != null)
            // {
            //     var rank1ManagerVariablesComp = rank1Manager.GetComponent<Variables>();
            //     if (rank1ManagerVariablesComp != null)
            //     {
            //         var managerPlayerGO = rank1ManagerVariablesComp.declarations.Get<GameObject>("managerPlayer");
            //         if (managerPlayerGO != null)
            //         {
            //             var managerPlayerVariablesComp = managerPlayerGO.GetComponent<Variables>();
            //             if (managerPlayerVariablesComp != null)
            //             {
            //                 var playerGO = managerPlayerVariablesComp.declarations.Get<GameObject>("playerGO");
            //                 if (playerGO != null)
            //                 {
            //                     _player = playerGO;
            //                 }
            //             }
            //         }
            //     }
            // }

            _player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            compassImage.uvRect = new Rect(_player.transform.localEulerAngles.y / 360.0f, 0.0f, 1.0f, 1.0f);

            foreach (var keyValuePair in _keyedIndications)
            {
                // if (keyValuePair.Key != null)
                // {
                    keyValuePair.Key.image.rectTransform.anchoredPosition = GetPosOnCompass(keyValuePair.Key);

                    var distance = Vector2.Distance(new Vector2(_player.transform.position.x, _player.transform.position.z),
                        keyValuePair.Key.MappedPos);
                    var scale = 0.0f;

                    if (distance < maxDistance)
                    {
                        scale = 1.0f - (distance / maxDistance);
                    }
                    keyValuePair.Key.image.rectTransform.localScale = Vector3.one * scale;
                // }
            }
        }

        public void AddIndication(UIIndication indication)
        {
            // var iconInstance =
            //     MyPooler.ObjectPooler.Instance.GetFromPool("Icon", Vector3.zero, Quaternion.identity);
            var iconInstance = GameObject.Instantiate(iconPrefab, compassImage.transform);

            indication.image = iconInstance.GetComponent<Image>();
            indication.image.sprite = indication.icon;

            // _indications.Add(indication);
            if (!_keyedIndications.ContainsKey(indication))
            {
                _keyedIndications.Add(indication, iconInstance);
            }
        }

        public void RemoveIndication(UIIndication indication)
        {
            // MyPooler.ObjectPooler.Instance.ReturnToPool("Icon", );

            if (_keyedIndications.ContainsKey(indication))
            {
                var iconInstance = _keyedIndications[indication];
                GameObject.Destroy(iconInstance);
                _keyedIndications.Remove(indication);
            }
        }


        Vector2 GetPosOnCompass(UIIndication indication)
        {
            var playerPos = new Vector2(_player.transform.position.x, _player.transform.position.z);
            var playerForward = new Vector2(_player.transform.forward.x, _player.transform.forward.z);

            var angle = Vector2.SignedAngle(indication.MappedPos - playerPos, playerForward);

            return new Vector2(_compassUnit * angle, 0);
        }
    }
}
