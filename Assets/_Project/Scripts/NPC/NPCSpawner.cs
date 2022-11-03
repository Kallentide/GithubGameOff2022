using GithubGameOff2022.SO;
using System.Collections;
using TMPro;
using UnityEngine;

namespace GithubGameOff2022.NPC
{
    public class NPCSpawner : MonoBehaviour
    {
        [SerializeField]
        private MonsterSpawnInfo _info;

        [SerializeField]
        private TMP_Text _text;

        private void Start()
        {
            TimeManager.Instance.OnReady.AddListener(new(() =>
            {
                StartCoroutine(SpawnMonsters());
            }));
        }

        private IEnumerator SpawnMonsters()
        {
            while (true)
            {
                Instantiate(_info.PossibleSpawns[0].Prefab, transform);
                _text.text = $"{_info.SpawnInterval}";
                for (var i = _info.SpawnInterval; i >= 0; i--)
                {
                    yield return new WaitForSeconds(1f);
                    _text.text = $"{i}";
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Monster"))
            {
                var npc = other.GetComponent<NPCController>();
                if (npc.IsLeaving)
                {
                    Destroy(npc.gameObject);
                }
            }
        }
    }
}
