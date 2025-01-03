using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        [SerializeField] private bool isPlayed = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player" && !isPlayed)
            {
                GetComponent<PlayableDirector>().Play();
                isPlayed = true;
            }
        }
    }
}
