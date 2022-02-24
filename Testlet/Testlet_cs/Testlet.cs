using System;
using System.Collections.Generic;
using System.Linq;

namespace Testlet_cs
{
    public class Testlet
    {
        public string TestletId;
        private readonly List<Item> Items;

        public Testlet(string testletId, List<Item> items)
        {
            TestletId = testletId;
            Items = items;
        }

        public List<Item> Randomize()
        {
            try
            {
                if (Items == null)
                    throw new ArgumentNullException("Items", "Testlet items cannot be empty!");

                if (Items.Count > 10)
                    throw new Exception("Testlet items cannot be more than 10!");

                if (Items.Count < 10)
                    throw new Exception("Testlet items cannot be less than 10!");

                Random random = new Random();

                //for => first 2 items are always pretest items randomly
                List<Item> rdnPretest = Items.Where(t => t.ItemType == ItemTypeEnum.Pretest).OrderBy(t => random.Next()).Take(2).ToList();

                //for => next 8 items are mix of pretest and operational items randomly
                List<Item> rdnRemaning = Items.Except(rdnPretest).OrderBy(t => random.Next()).ToList();

                List<Item> output = rdnPretest.Concat(rdnRemaning).ToList();

                return output;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class Item
    {
        public string ItemId;
        public ItemTypeEnum ItemType;
    }

    public enum ItemTypeEnum
    {
        Pretest,
        Operational
    }

}