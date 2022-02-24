using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit.Framework;
using System;
using System.Collections.Generic;
using Testlet_cs;

namespace UnitTestProject_Testlet
{
    #region Custom Class
    internal static class CustomAssert
    {
        public static void IsFirst2Pretests(this Assert assert, List<Item> actuallst)
        {
            if ((actuallst[0].ItemType == ItemTypeEnum.Pretest) && (actuallst[1].ItemType == ItemTypeEnum.Pretest))
                return;

            throw new AssertFailedException($"First 2 Testlet Items are not of Pretest Type! - 1->{actuallst[0].ItemType}, 2->{actuallst[1].ItemType}");
        }
        public static void IfMorethan10Items(this Assert assert, Testlet testlet)
        {
            try
            {
                List<Item> items = testlet.Randomize();
                return;
            }
            catch (Exception ex)
            {
                throw new AssertFailedException(ex.Message);
            }
            
        }
        public static void IfLessthan10Items(this Assert assert, Testlet testlet)
        {
            try
            {
                List<Item> items = testlet.Randomize();
                return;
            }
            catch (Exception ex)
            {
                throw new AssertFailedException(ex.Message);
            }

        }
    }
    #endregion


    [TestClass]
    public class TestletTest
    {
        #region Get Testlet Items
        private List<Item> GetWorkingTestlets()
        {
            List<Item> testlets = new List<Item>
            {
                //Add some sample Testlets
                new Item() { ItemId = "1", ItemType = ItemTypeEnum.Operational },
                new Item() { ItemId = "2", ItemType = ItemTypeEnum.Operational },
                new Item() { ItemId = "3", ItemType = ItemTypeEnum.Pretest },
                new Item() { ItemId = "4", ItemType = ItemTypeEnum.Pretest },
                new Item() { ItemId = "5", ItemType = ItemTypeEnum.Pretest },
                new Item() { ItemId = "6", ItemType = ItemTypeEnum.Pretest },
                new Item() { ItemId = "7", ItemType = ItemTypeEnum.Operational },
                new Item() { ItemId = "8", ItemType = ItemTypeEnum.Operational },
                new Item() { ItemId = "9", ItemType = ItemTypeEnum.Operational },
                new Item() { ItemId = "10", ItemType = ItemTypeEnum.Operational }
            };

            return testlets;
        }
        private List<Item> GetLessThan10Testlets()
        {
            List<Item> testlets = new List<Item>
            {
                //Add some sample Testlets
                new Item() { ItemId = "1", ItemType = ItemTypeEnum.Operational },
                new Item() { ItemId = "2", ItemType = ItemTypeEnum.Operational },
                new Item() { ItemId = "3", ItemType = ItemTypeEnum.Pretest },
                new Item() { ItemId = "4", ItemType = ItemTypeEnum.Pretest },
                new Item() { ItemId = "5", ItemType = ItemTypeEnum.Pretest },
                new Item() { ItemId = "6", ItemType = ItemTypeEnum.Pretest },
                new Item() { ItemId = "7", ItemType = ItemTypeEnum.Operational }
            };

            return testlets;
        }
        private List<Item> GetMoreThan10Testlets()
        {
            List<Item> testlets = new List<Item>
            {
                //Add some sample Testlets
                new Item() { ItemId = "1", ItemType = ItemTypeEnum.Operational },
                new Item() { ItemId = "2", ItemType = ItemTypeEnum.Operational },
                new Item() { ItemId = "3", ItemType = ItemTypeEnum.Pretest },
                new Item() { ItemId = "4", ItemType = ItemTypeEnum.Pretest },
                new Item() { ItemId = "5", ItemType = ItemTypeEnum.Pretest },
                new Item() { ItemId = "6", ItemType = ItemTypeEnum.Pretest },
                new Item() { ItemId = "7", ItemType = ItemTypeEnum.Operational },
                new Item() { ItemId = "8", ItemType = ItemTypeEnum.Operational },
                new Item() { ItemId = "9", ItemType = ItemTypeEnum.Operational },
                new Item() { ItemId = "10", ItemType = ItemTypeEnum.Operational },
                new Item() { ItemId = "11", ItemType = ItemTypeEnum.Operational },
                new Item() { ItemId = "12", ItemType = ItemTypeEnum.Pretest }
            };

            return testlets;
        }
        #endregion

        #region Failing Test Cases
        [TestMethod]
        public void WhenEmptyTestletsPassed()
        {

            Testlet tl = new Testlet("1", null);

            Assert.IsNotNull(tl.Randomize());

        }
        [TestMethod]
        public void CheckIf_First2Are_OperationalItems()
        {

            Testlet tl = new Testlet("1", GetWorkingTestlets());

            // is first item pretest
            Assert.IsTrue(tl.Randomize()[0].ItemType == ItemTypeEnum.Operational, "First Item is not Pretest!");

            // is second item pretest
            Assert.IsTrue(tl.Randomize()[1].ItemType == ItemTypeEnum.Operational, "Second Item is not Pretest!");

        }
        [TestMethod]
        public void IsEqual()//This test should fail
        {

            List<Item> testlets = GetWorkingTestlets();

            Testlet tl = new Testlet("1", testlets);

            CollectionAssert.AreEqual(tl.Randomize(), testlets, "Records in Expected set are Randomized, So it cannot be Equal!");

        }
        [TestMethod]
        public void CheckIf_MoreThan10TestletItems()
        {

            Testlet tl = new Testlet("1", GetMoreThan10Testlets());

            Assert.That.IfMorethan10Items(tl);

        }
        [TestMethod]
        public void CheckIf_LessThan10TestletItems()
        {

            Testlet tl = new Testlet("1", GetLessThan10Testlets());

            Assert.That.IfMorethan10Items(tl);

        }
        #endregion

        #region Passed Test Cases
        [TestMethod]
        public void CheckIf_First2Are_PretestItems() // Check using custom assert method
        {

            Testlet tl = new Testlet("1", GetWorkingTestlets());

            Assert.That.IsFirst2Pretests(tl.Randomize());

        }
        [TestMethod]
        public void IsCountMatches()
        {

            List<Item> testlets = GetWorkingTestlets();

            Testlet tl = new Testlet("1", GetWorkingTestlets());

            Assert.AreEqual(tl.Randomize().Count, testlets.Count, "All Items are not returned!");

        }
        [TestMethod]
        public void IsEquivalent()
        {

            List<Item> testlets = GetWorkingTestlets();

            Testlet tl = new Testlet("1", testlets);

            CollectionAssert.AreEquivalent(tl.Randomize(), testlets, "No of records in Expected set matches with Actual set");

        }
        [TestMethod]
        public void IsNotNullResultSet()
        {

            Testlet tl = new Testlet("1", GetWorkingTestlets());

            Assert.IsNotNull(tl.Randomize(), "Expected Result set is null!");

        }
        [TestMethod]
        public void IsAllItemsUnique()
        {

            Testlet tl = new Testlet("1", GetWorkingTestlets());

            CollectionAssert.AllItemsAreUnique(tl.Randomize(), "Duplicate Items found!");

        }
        #endregion

    }
}
