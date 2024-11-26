using Battleground.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battleground
{
    public interface ICardHolder
    {
        public void SelectCardEvent();

        public bool Add(UICard card);

        public void Remove(UICard removedCard);
    }
}