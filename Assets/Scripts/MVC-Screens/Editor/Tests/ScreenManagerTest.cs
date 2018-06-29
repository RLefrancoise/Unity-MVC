using NUnit.Framework;

namespace Mvc.Screens.Tests
{
    public class ScreenManagerTest
    {
        [SetUp]
        public void BeforeTest()
        {
            ScreenManager.Instance.ClearScreens();
        }

        [Test]
        public void TestPopEmptyStack()
        {
            Assert.IsNull(ScreenManager.Instance.PopScreen());
        }

        [Test]
        public void TestPushSingleScreen()
        {
            Assert.IsNull(ScreenManager.Instance.CurrentScreen);

            ScreenStub screen = new ScreenStub();

            Assert.AreSame(ScreenManager.Instance.PushScreen(screen), screen);
            Assert.IsNotNull(ScreenManager.Instance.CurrentScreen);

            Assert.IsTrue(screen.Created);
            Assert.IsTrue(screen.IsVisible);
            Assert.IsFalse(screen.Destroyed);

            Assert.That(ScreenManager.Instance.NumberOfScreens == 1);
        }

        [Test]
        public void TestPushMultipleScreens()
        {
            Assert.IsNull(ScreenManager.Instance.CurrentScreen);

            ScreenStub screen1 = new ScreenStub();
            ScreenStub screen2 = new ScreenStub();

            //Push screen1
            Assert.AreSame(screen1, ScreenManager.Instance.PushScreen(screen1));
            Assert.AreSame(screen1, ScreenManager.Instance.CurrentScreen);

            Assert.IsTrue(screen1.Created);
            Assert.IsTrue(screen1.IsVisible);
            Assert.IsFalse(screen1.Destroyed);

            Assert.That(ScreenManager.Instance.NumberOfScreens == 1);

            //Push screen2
            Assert.AreSame(screen2, ScreenManager.Instance.PushScreen(screen2));
            Assert.AreSame(screen2, ScreenManager.Instance.CurrentScreen);

            Assert.IsFalse(screen1.IsVisible);

            Assert.IsTrue(screen2.Created);
            Assert.IsTrue(screen2.IsVisible);
            Assert.IsFalse(screen2.Destroyed);

            Assert.That(ScreenManager.Instance.NumberOfScreens == 2);
        }

        [Test]
        public void TestPopScreenSingleScreen()
        {
            ScreenStub screen = new ScreenStub();

            ScreenManager.Instance.PushScreen(screen);

            Assert.That(ScreenManager.Instance.NumberOfScreens == 1);

            IScreen poppedScreen = ScreenManager.Instance.PopScreen();

            Assert.AreSame(screen, poppedScreen);
            Assert.IsTrue(screen.Destroyed);
            Assert.IsFalse(screen.Created);
            Assert.IsFalse(screen.IsVisible);

            Assert.That(ScreenManager.Instance.NumberOfScreens == 0);
        }

        [Test]
        public void TestPopScreenMultipleScreens()
        {
            ScreenStub screen1 = new ScreenStub();
            ScreenStub screen2 = new ScreenStub();

            ScreenManager.Instance.PushScreen(screen1);
            ScreenManager.Instance.PushScreen(screen2);

            Assert.AreSame(ScreenManager.Instance.CurrentScreen, screen2);
            Assert.That(ScreenManager.Instance.NumberOfScreens == 2);

            //Pop screen 2
            Assert.AreSame(screen2, ScreenManager.Instance.PopScreen());
            Assert.AreSame(screen1, ScreenManager.Instance.CurrentScreen);
            
            Assert.IsTrue(screen1.Created);
            Assert.IsTrue(screen1.IsVisible);
            Assert.IsFalse(screen1.Destroyed);

            Assert.IsTrue(screen2.Destroyed);
            Assert.IsFalse(screen2.IsVisible);
            Assert.IsFalse(screen2.Created);

            Assert.That(ScreenManager.Instance.NumberOfScreens == 1);

            //Pop screen 1
            Assert.AreSame(screen1, ScreenManager.Instance.PopScreen());
            Assert.IsNull(ScreenManager.Instance.CurrentScreen);

            Assert.IsFalse(screen1.Created);
            Assert.IsTrue(screen1.Destroyed);
            Assert.IsFalse(screen1.IsVisible);

            Assert.That(ScreenManager.Instance.NumberOfScreens == 0);
        }

        [Test]
        public void TestSetSingleScreen()
        {
            Assert.IsNull(ScreenManager.Instance.CurrentScreen);

            ScreenStub screen = new ScreenStub();

            ScreenManager.Instance.SetScreen(screen);

            Assert.AreSame(screen, ScreenManager.Instance.CurrentScreen);
            Assert.IsTrue(screen.Created);
            Assert.IsTrue(screen.IsVisible);
            Assert.IsFalse(screen.Destroyed);

            Assert.That(ScreenManager.Instance.NumberOfScreens == 1);
        }

        [Test]
        public void TestSetMultipleScreens()
        {
            Assert.IsNull(ScreenManager.Instance.CurrentScreen);

            ScreenStub screen1 = new ScreenStub();
            ScreenStub screen2 = new ScreenStub();

            //Set screen 1
            ScreenManager.Instance.SetScreen(screen1);

            Assert.AreSame(screen1, ScreenManager.Instance.CurrentScreen);
            Assert.IsTrue(screen1.Created);
            Assert.IsTrue(screen1.IsVisible);
            Assert.IsFalse(screen1.Destroyed);

            Assert.That(ScreenManager.Instance.NumberOfScreens == 1);

            //Set screen 2
            ScreenManager.Instance.SetScreen(screen2);

            Assert.AreSame(screen2, ScreenManager.Instance.CurrentScreen);
            Assert.IsTrue(screen2.Created);
            Assert.IsTrue(screen2.IsVisible);
            Assert.IsFalse(screen2.Destroyed);

            Assert.IsFalse(screen1.Created);
            Assert.IsTrue(screen1.Destroyed);
            Assert.IsFalse(screen1.IsVisible);

            Assert.That(ScreenManager.Instance.NumberOfScreens == 1);
        }

        [Test]
        public void TestClearScreens()
        {
            Assert.IsNull(ScreenManager.Instance.CurrentScreen);

            ScreenStub screen1 = new ScreenStub();
            ScreenStub screen2 = new ScreenStub();
            ScreenStub screen3 = new ScreenStub();

            ScreenManager.Instance.PushScreen(screen1);
            ScreenManager.Instance.PushScreen(screen2);
            ScreenManager.Instance.PushScreen(screen3);

            Assert.IsTrue(screen1.Created);
            Assert.IsFalse(screen1.IsVisible);
            Assert.IsFalse(screen1.Destroyed);

            Assert.IsTrue(screen2.Created);
            Assert.IsFalse(screen2.IsVisible);
            Assert.IsFalse(screen2.Destroyed);

            Assert.IsTrue(screen3.Created);
            Assert.IsTrue(screen3.IsVisible);
            Assert.IsFalse(screen3.Destroyed);

            Assert.That(ScreenManager.Instance.NumberOfScreens == 3);

            ScreenManager.Instance.ClearScreens();

            Assert.IsFalse(screen1.Created);
            Assert.IsFalse(screen1.IsVisible);
            Assert.IsTrue(screen1.Destroyed);

            Assert.IsFalse(screen2.Created);
            Assert.IsFalse(screen2.IsVisible);
            Assert.IsTrue(screen2.Destroyed);

            Assert.IsFalse(screen3.Created);
            Assert.IsFalse(screen3.IsVisible);
            Assert.IsTrue(screen3.Destroyed);

            Assert.That(ScreenManager.Instance.NumberOfScreens == 0);
        }
    }
}
