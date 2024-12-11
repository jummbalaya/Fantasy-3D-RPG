using UnityEngine;
using System;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover mover;
        private Fighter fighter;

        private void Start()
        {
            mover = GetComponent<Mover>();
            fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            //order of methods is important, done change it
            //uses the fighter script
            if (InteractWithCombat()) return;
            //uses the mover script
            if (InteractWithMovement()) return;
            print ("Nothing to do here.");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                CombatTarget combatTarget = hit.transform.GetComponent<CombatTarget>();
                if (!fighter.CanAttack(combatTarget)) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    fighter.Attack(combatTarget);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    mover.StartMoveAction(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

