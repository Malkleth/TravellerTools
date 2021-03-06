﻿using System.Collections.Generic;

namespace Grauenwolf.TravellerTools.Equipment
{
    public class StoreOptions
    {
        public StoreOptions()
        {
        }

        public EHex LawLevel { get; set; }
        public EHex TechLevel { get; set; }
        public EHex Starport { get; set; }
        public List<string> TradeCodes { get; } = new List<string>();
        public EHex Population { get; set; }

        public int BrokerScore { get; set; }
        public int StreetwiseScore { get; set; }

        public bool AutoRoll { get; set; }
        public bool DiscountPrices { get; set; }
        public int? Seed { get; set; }

        public bool WeaponsRestricted { get; set; }
        public bool DrugsRestricted { get; set; }
        public bool PsionicsRestricted { get; set; }
        public bool TechnologyRestricted { get; set; }
        public bool InformationRestricted { get; set; }
    }
}
