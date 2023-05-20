using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hullbreakers
{
    [RequireComponent(typeof(UnitReference))]
    public class Upgradeable : MonoBehaviour
    {
        public enum UpgradePath
        {
            Top,
            Mid,
            Bot
        }

        public readonly int[] Splits = {
            0,
            0,
            0
        };

        readonly int[] _maxSplits =
        {
            5,
            5,
            5
        };

        public UnitSo selfUnit { get; private set; }

        int _netWorth;

        public int sellbackValue => (int)(_netWorth * 0.7f);
        
        List<List<Upgrade>> _upgrades;

        public int TierOnPath(UpgradePath path)
        {
            return Splits[(int)path];
        }

        public Upgrade GetCurrentUpgradeOnPath(UpgradePath path)
        {
            return TierOnPath(path) == 0 ? null : _upgrades[(int)path][TierOnPath(path) - 1];
        }
        
        public Upgrade GetNextUpgradeOnPath(UpgradePath path)
        {
            return !IsValidUpgradePath(path) ? null : _upgrades[(int)path][TierOnPath(path)];
        }

        void LockPath(UpgradePath path)
        {
            for(int i = 0; i < _maxSplits.Length; i++)
            {
                if((int)path == i) continue;
                _maxSplits[i] = Math.Min(_maxSplits[i], 2);
            }
        }

        int NonZeroPathCount()
        {
            int pathcount = 0;

            foreach (int path in Splits)
            {
                if (path > 0) pathcount++;
            }

            return pathcount;
        }

        void TryRemoveOutlierPath()
        {
            if(NonZeroPathCount() < 2) return;
            
            for(int i = 0; i < Splits.Length; i++)
            {
                if (Splits[i] != 0) continue;
                _maxSplits[i] = 0;
                return;
            }
        }

        public bool PathLocked(UpgradePath path)
        {
            return _maxSplits[(int)path] == 0;
        }

        bool IsValidUpgradePath(UpgradePath path)
        {
            return Splits[(int)path] < _maxSplits[(int)path];
        }

        public bool CanAffordUpgrade(Upgrade upgrade)
        {
            return PlayerOrbs.instance.currency.cash >= upgrade.price;
        }

        public void PurchaseUpgrade(UpgradePath upgradePath)
        {
            Upgrade selectedUpgrade = GetNextUpgradeOnPath(upgradePath);
            
            if (!IsValidUpgradePath(upgradePath))
            {
                throw new InvalidOperationException("Invalid Upgrade Path");
            }

            if (!CanAffordUpgrade(selectedUpgrade))
            {
                throw new InvalidOperationException("Cannot afford Upgrade!");
            }

            PlayerOrbs.instance.currency.cash -= selectedUpgrade.price;
            selectedUpgrade.Do(gameObject);
            Splits[(int)upgradePath]++;
            _netWorth += selectedUpgrade.price;
            
            if (Splits[(int)upgradePath] == 3)
            {
                LockPath(upgradePath);
            }
            
            TryRemoveOutlierPath();
        }

        void Awake()
        {
            selfUnit = GetComponent<UnitReference>().selfUnit;
            
            _upgrades = new List<List<Upgrade>>()
            {
                selfUnit.Top,
                selfUnit.Mid,
                selfUnit.Bot,
            };

            _netWorth = selfUnit.price;
        }
    }
}
