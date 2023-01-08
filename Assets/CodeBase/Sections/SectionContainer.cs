using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CodeBase.Sections
{
    public class SectionContainer : MonoBehaviour
    {
        [SerializeField] private List<SectionDynamic> sections;

        [SerializeField] private Camera camera;


        private void Awake()
        {
            sections.ForEach(section =>
            {
                section.SetupLengthSections(sections.Count);
                section.OnFarSection += OnSectionFar;
            });
        }

        private void OnSectionFar(SectionDynamic obj)
        {
            if (obj.transform.position.x < camera.transform.position.x)
            {
                SwipeLeftSectionToRight();
            }
            else
            {
                SwipeRightSectionToLeft();
            }

            obj.SetActive(true);
        }

        [Button]
        private void SortSections()
        {
            sections = sections.OrderBy(x => x.transform.position.x - camera.transform.position.x).ToList();
        }

        [Button]
        private void SwipeLeftSectionToRight()
        {
            var section = sections[0];

            var posLast = sections.Last().transform.position.x;

            sections.RemoveAt(0);

            sections.Add(section);

            //section.SetPosition(posLast + section.LengthSection);
        }

        [Button]
        private void SwipeRightSectionToLeft()
        {
            var section = sections.Last();

            var posLast = sections[0].transform.position.x;

            sections.RemoveAt(sections.Count - 1);

            sections.Insert(0, section);

           // section.SetPosition(posLast - section.LengthSection);
        }
    }
}