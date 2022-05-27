using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FishGame.Systems
{
    public class CurrencySystem : MonoBehaviour
    {
        private static CurrencySystem _instance;

        public static CurrencySystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CurrencySystem();
                }
                return _instance;
            }
        }
        
        int coins, gems;

        //public UnityEvent OnCurrencyUpdated;
       
        public CurrencySystem()
        {
            _instance = this;
            _instance.coins = 0;
            _instance.gems = 0;
           
        }


        //public void AddCoins(int amount)
        //{
        //    if (amount <= 0) return;
        //    coins += amount;
        //    OnCoinsAdded?.Invoke();
        //}

        //public void AddGems(int amount)
        //{
        //    if (amount <= 0) return;
        //    gems += amount;
        //    OnGemsAdded?.Invoke();
        //}

        //public void SubtractCoins(int amount)
        //{
        //    if (amount > coins) return;
        //    coins -= amount;
        //    OnCoinsAdded?.Invoke();
        //}

        //public void SubtractGems(int amount)
        //{
        //    if (amount > gems) return;
        //    gems -= amount;
        //    OnCoinsAdded?.Invoke();
        //}

        //// used for initliaze the class
        //public void SetCoin(int coinsAmount)
        //{
        //    coins = coinsAmount;
        //}

        //public void SetGems(int gemsAmount)
        //{
        //    gems = gemsAmount;
        //}
        //////////////////////////////
        public int GetCoins()
        {
            return coins;
        }

        public int GetGems()
        {
            return gems;
        }

        public void InitCurrencySystem(int cn,int gm)
        {
            coins = cn;
            gems = gm;
        }

    }

}