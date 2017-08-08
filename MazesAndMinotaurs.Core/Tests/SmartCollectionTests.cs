using NUnit.Framework;
using System.Collections.ObjectModel;

namespace MazesAndMinotaurs.Core.Tests
{
	[TestFixture]
	internal class SmartCollectionTests
	{
		[Test]
		public void OneItemOneCollectionAddTest()
		{
			var sc = new SmartCollection<TestItem, object>(new object());
			var si = new TestItem();

			Assert.AreEqual(null, si.Collection);

			sc.Add(si);

			Assert.AreEqual(sc, si.Collection);
			Assert.IsTrue(sc.Contains(si));
		}

		[Test]
		public void OneItemOneCollectionRemoveTest()
		{
			var sc = new SmartCollection<TestItem, object>(new object());
			var si = new TestItem();
			Assert.AreEqual(null, si.Collection);
			sc.Add(si);
			Assert.AreEqual(sc, si.Collection);

			sc.Remove(si);

			Assert.AreEqual(null, si.Collection);
			Assert.IsFalse(sc.Contains(si));
		}

		[Test]
		public void OneItemOneCollectionPropertyTest()
		{
			var sc = new SmartCollection<TestItem, object>(new object());
			var si = new TestItem();

			Assert.AreEqual(null, si.Collection);

			si.Collection = sc;

			Assert.AreEqual(sc, si.Collection);
			Assert.IsTrue(sc.Contains(si));

			si.Collection = null;

			Assert.AreEqual(null, si.Collection);
			Assert.IsFalse(sc.Contains(si));
		}

		[Test]
		public void OneItemTwoCollectionsAddTest()
		{
			var sc1 = new SmartCollection<TestItem, object>(new object());
			var sc2 = new SmartCollection<TestItem, object>(new object());
			var si = new TestItem();
			sc1.Add(si);
			sc2.Add(si);

			Assert.AreEqual(sc2, si.Collection);
			Assert.IsFalse(sc1.Contains(si));
			Assert.IsTrue(sc2.Contains(si));
		}

		[Test]
		public void OneItemTwoCollectionsPropertyTest()
		{
			var sc1 = new SmartCollection<TestItem, object>(new object());
			var sc2 = new SmartCollection<TestItem, object>(new object());
			var si = new TestItem();
			si.Collection = sc1;
			si.Collection = sc2;

			Assert.AreEqual(sc2, si.Collection);
			Assert.IsFalse(sc1.Contains(si));
			Assert.IsTrue(sc2.Contains(si));
		}

		[Test]
		public void ResetTest()
		{
			var sc = new SmartCollection<TestItem, object>(new object());
			var si1 = new TestItem();
			var si2 = new TestItem();
			var si3 = new TestItem();

			sc.Add(si1);
			si2.Collection = sc;
			sc.Add(si3);

			Assert.AreEqual(sc, si1.Collection);
			Assert.AreEqual(sc, si2.Collection);
			Assert.AreEqual(sc, si3.Collection);

			Assert.IsTrue(sc.Contains(si1));
			Assert.IsTrue(sc.Contains(si2));
			Assert.IsTrue(sc.Contains(si3));

			sc.Clear();

			Assert.AreEqual(null, si1.Collection);
			Assert.AreEqual(null, si2.Collection);
			Assert.AreEqual(null, si3.Collection);

			Assert.IsFalse(sc.Contains(si1));
			Assert.IsFalse(sc.Contains(si2));
			Assert.IsFalse(sc.Contains(si3));
		}

		private class TestItem : ICollectionItem<TestItem>
		{
			ObservableCollection<TestItem> _collection;

			public ObservableCollection<TestItem> Collection
			{
				get
				{
					return _collection;
				}
				set
				{
					if (_collection != value)
					{
						var collection = _collection;
						_collection = null;
						if (collection != null && collection.Count > 0)
						{
							collection.Remove(this);
						}
						_collection = value;
						if (_collection != null && !_collection.Contains(this))
						{
							_collection.Add(this);
						}
					}
				}
			}
		}
	}
}
