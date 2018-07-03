using NUnit.Framework;

namespace Mvc.Screens.Tests
{
    public class ScreenManagerTest
    {
        private ScreenManager _screenManager;

        [OneTimeSetUp]
        public void Init()
        {
            _screenManager = new ScreenManager();    
        }

        [SetUp]
        public void BeforeTest()
        {
            _screenManager.ClearScreens();
        }

        [Test]
        public void TestPopEmptyStack()
        {
            Assert.IsNull(_screenManager.PopScreen());
        }

        [Test]
        public void TestPushSingleScreen()
        {
            Assert.IsNull(_screenManager.CurrentScreen);

            ScreenStub screen = new ScreenStub();

            Assert.AreSame(_screenManager.PushScreen(screen), screen);
            Assert.IsNotNull(_screenManager.CurrentScreen);

            Assert.IsTrue(screen.Created);
            Assert.IsTrue(screen.IsVisible);
            Assert.IsFalse(screen.Destroyed);

            Assert.That(_screenManager.NumberOfScreens == 1);
        }

        [Test]
        public void TestPushMultipleScreens()
        {
            Assert.IsNull(_screenManager.CurrentScreen);

            ScreenStub screen1 = new ScreenStub();
            ScreenStub screen2 = new ScreenStub();

            //Push screen1
            Assert.AreSame(screen1, _screenManager.PushScreen(screen1));
            Assert.AreSame(screen1, _screenManager.CurrentScreen);

            Assert.IsTrue(screen1.Created);
            Assert.IsTrue(screen1.IsVisible);
            Assert.IsFalse(screen1.Destroyed);

            Assert.That(_screenManager.NumberOfScreens == 1);

            //Push screen2
            Assert.AreSame(screen2, _screenManager.PushScreen(screen2));
            Assert.AreSame(screen2, _screenManager.CurrentScreen);

            Assert.IsFalse(screen1.IsVisible);

            Assert.IsTrue(screen2.Created);
            Assert.IsTrue(screen2.IsVisible);
            Assert.IsFalse(screen2.Destroyed);

            Assert.That(_screenManager.NumberOfScreens == 2);
        }

        [Test]
        public void TestPopScreenSingleScreen()
        {
            ScreenStub screen = new ScreenStub();

            _screenManager.PushScreen(screen);

            Assert.That(_screenManager.NumberOfScreens == 1);

            IScreen poppedScreen = _screenManager.PopScreen();

            Assert.AreSame(screen, poppedScreen);
            Assert.IsTrue(screen.Destroyed);
            Assert.IsFalse(screen.Created);
            Assert.IsFalse(screen.IsVisible);

            Assert.That(_screenManager.NumberOfScreens == 0);
        }

        [Test]
        public void TestPopScreenMultipleScreens()
        {
            ScreenStub screen1 = new ScreenStub();
            ScreenStub screen2 = new ScreenStub();

            _screenManager.PushScreen(screen1);
            _screenManager.PushScreen(screen2);

            Assert.AreSame(_screenManager.CurrentScreen, screen2);
            Assert.That(_screenManager.NumberOfScreens == 2);

            //Pop screen 2
            Assert.AreSame(screen2, _screenManager.PopScreen());
            Assert.AreSame(screen1, _screenManager.CurrentScreen);
            
            Assert.IsTrue(screen1.Created);
            Assert.IsTrue(screen1.IsVisible);
            Assert.IsFalse(screen1.Destroyed);

            Assert.IsTrue(screen2.Destroyed);
            Assert.IsFalse(screen2.IsVisible);
            Assert.IsFalse(screen2.Created);

            Assert.That(_screenManager.NumberOfScreens == 1);

            //Pop screen 1
            Assert.AreSame(screen1, _screenManager.PopScreen());
            Assert.IsNull(_screenManager.CurrentScreen);

            Assert.IsFalse(screen1.Created);
            Assert.IsTrue(screen1.Destroyed);
            Assert.IsFalse(screen1.IsVisible);

            Assert.That(_screenManager.NumberOfScreens == 0);
        }

        [Test]
        public void TestSetSingleScreen()
        {
            Assert.IsNull(_screenManager.CurrentScreen);

            ScreenStub screen = new ScreenStub();

            _screenManager.SetScreen(screen);

            Assert.AreSame(screen, _screenManager.CurrentScreen);
            Assert.IsTrue(screen.Created);
            Assert.IsTrue(screen.IsVisible);
            Assert.IsFalse(screen.Destroyed);

            Assert.That(_screenManager.NumberOfScreens == 1);
        }

        [Test]
        public void TestSetMultipleScreens()
        {
            Assert.IsNull(_screenManager.CurrentScreen);

            ScreenStub screen1 = new ScreenStub();
            ScreenStub screen2 = new ScreenStub();

            //Set screen 1
            _screenManager.SetScreen(screen1);

            Assert.AreSame(screen1, _screenManager.CurrentScreen);
            Assert.IsTrue(screen1.Created);
            Assert.IsTrue(screen1.IsVisible);
            Assert.IsFalse(screen1.Destroyed);

            Assert.That(_screenManager.NumberOfScreens == 1);

            //Set screen 2
            _screenManager.SetScreen(screen2);

            Assert.AreSame(screen2, _screenManager.CurrentScreen);
            Assert.IsTrue(screen2.Created);
            Assert.IsTrue(screen2.IsVisible);
            Assert.IsFalse(screen2.Destroyed);

            Assert.IsFalse(screen1.Created);
            Assert.IsTrue(screen1.Destroyed);
            Assert.IsFalse(screen1.IsVisible);

            Assert.That(_screenManager.NumberOfScreens == 1);
        }

        [Test]
        public void TestClearScreens()
        {
            Assert.IsNull(_screenManager.CurrentScreen);

            ScreenStub screen1 = new ScreenStub();
            ScreenStub screen2 = new ScreenStub();
            ScreenStub screen3 = new ScreenStub();

            _screenManager.PushScreen(screen1);
            _screenManager.PushScreen(screen2);
            _screenManager.PushScreen(screen3);

            Assert.IsTrue(screen1.Created);
            Assert.IsFalse(screen1.IsVisible);
            Assert.IsFalse(screen1.Destroyed);

            Assert.IsTrue(screen2.Created);
            Assert.IsFalse(screen2.IsVisible);
            Assert.IsFalse(screen2.Destroyed);

            Assert.IsTrue(screen3.Created);
            Assert.IsTrue(screen3.IsVisible);
            Assert.IsFalse(screen3.Destroyed);

            Assert.That(_screenManager.NumberOfScreens == 3);

            _screenManager.ClearScreens();

            Assert.IsFalse(screen1.Created);
            Assert.IsFalse(screen1.IsVisible);
            Assert.IsTrue(screen1.Destroyed);

            Assert.IsFalse(screen2.Created);
            Assert.IsFalse(screen2.IsVisible);
            Assert.IsTrue(screen2.Destroyed);

            Assert.IsFalse(screen3.Created);
            Assert.IsFalse(screen3.IsVisible);
            Assert.IsTrue(screen3.Destroyed);

            Assert.That(_screenManager.NumberOfScreens == 0);
        }
    }
}
