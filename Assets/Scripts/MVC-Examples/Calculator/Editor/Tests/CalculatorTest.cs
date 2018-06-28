// This file is part of the Unity-MVC Project
// https://github.com/RLefrancoise/Unity-MVC
// 
// BSD 3-Clause License
// 
// Copyright (c) 2018, Renaud Lefrancoise
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
// 
// * Redistributions of source code must retain the above copyright notice, this
//   list of conditions and the following disclaimer.
// 
// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution.
// 
// * Neither the name of the copyright holder nor the names of its
//   contributors may be used to endorse or promote products derived from
//   this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
using System;
using NUnit.Framework;

namespace Mvc.Examples.Calculator.Tests
{
    public class CalculatorTest
    {
        private ICalculatorApplication application;
        private ICalculatorController controller;
        private CalculatorTestView view;

        [OneTimeSetUp]
        public void Init()
        {
            application = new CalculatorTestApplication();
            controller = application.ControllerFactory.Controllers[0] as ICalculatorController;
            view = controller.View as CalculatorTestView;
        }

        [TearDown]
        public void TearDown()
        {
            view.RequestClear();
        }

        [Test]
        public void TestClear()
        {
            view.SimulateKeyPress(6);
            view.SimulateKeyPress("+");
            view.SimulateKeyPress(2);

            view.RequestClear();

            Assert.IsTrue(view.FirstOperand == 0);
            Assert.IsTrue(view.SecondOperand == 0);
            Assert.IsTrue(view.Operation == CalculatorOperation.Unknown);
            Assert.IsTrue(view.Result == 0f);
        }

        [Test]
        public void TestAddOneDigit()
        {
            int firstOperand = new Random().Next(0, 10);
            int secondOperand = new Random().Next(0, 10);
            int result = firstOperand + secondOperand;

            view.SimulateKeyPress(firstOperand);
            view.SimulateKeyPress("+");
            view.SimulateKeyPress(secondOperand);
            view.RequestResult();

            Assert.That(view.Result == result);
        }

        [Test]
        public void TestAddTwoDigits()
        {
            int firstOperand = new Random().Next(10, 100);
            int secondOperand = new Random().Next(10, 100);
            int result = firstOperand + secondOperand;

            OperandToKeyStrokes(firstOperand);
            view.SimulateKeyPress("+");
            OperandToKeyStrokes(secondOperand);
            view.RequestResult();

            Assert.That(view.Result == result);
        }

        [Test]
        public void TestAddThreeDigits()
        {
            int firstOperand = new Random().Next(100, 1000);
            int secondOperand = new Random().Next(100, 1000);
            int result = firstOperand + secondOperand;

            OperandToKeyStrokes(firstOperand);
            view.SimulateKeyPress("+");
            OperandToKeyStrokes(secondOperand);
            view.RequestResult();

            Assert.That(view.Result == result);
        }

        [Test]
        public void TestSubOneDigit()
        {
            int firstOperand = new Random().Next(0, 10);
            int secondOperand = new Random().Next(0, 10);
            int result = firstOperand - secondOperand;

            view.SimulateKeyPress(firstOperand);
            view.SimulateKeyPress("-");
            view.SimulateKeyPress(secondOperand);
            view.RequestResult();

            Assert.That(view.Result == result);
        }

        [Test]
        public void TestSubTwoDigits()
        {
            int firstOperand = new Random().Next(10, 100);
            int secondOperand = new Random().Next(10, 100);
            int result = firstOperand - secondOperand;

            OperandToKeyStrokes(firstOperand);
            view.SimulateKeyPress("-");
            OperandToKeyStrokes(secondOperand);
            view.RequestResult();

            Assert.That(view.Result == result);
        }

        [Test]
        public void TestSubThreeDigits()
        {
            int firstOperand = new Random().Next(100, 1000);
            int secondOperand = new Random().Next(100, 1000);
            int result = firstOperand - secondOperand;

            OperandToKeyStrokes(firstOperand);
            view.SimulateKeyPress("-");
            OperandToKeyStrokes(secondOperand);
            view.RequestResult();

            Assert.That(view.Result == result);
        }

        [Test]
        public void TestMulOneDigit()
        {
            int firstOperand = new Random().Next(0, 10);
            int secondOperand = new Random().Next(0, 10);
            int result = firstOperand * secondOperand;

            view.SimulateKeyPress(firstOperand);
            view.SimulateKeyPress("*");
            view.SimulateKeyPress(secondOperand);
            view.RequestResult();

            Assert.That(view.Result == result);
        }

        [Test]
        public void TestMulTwoDigits()
        {
            int firstOperand = new Random().Next(10, 100);
            int secondOperand = new Random().Next(10, 100);
            int result = firstOperand * secondOperand;

            OperandToKeyStrokes(firstOperand);
            view.SimulateKeyPress("*");
            OperandToKeyStrokes(secondOperand);
            view.RequestResult();

            Assert.That(view.Result == result);
        }

        [Test]
        public void TestMulThreeDigits()
        {
            int firstOperand = new Random().Next(100, 1000);
            int secondOperand = new Random().Next(100, 1000);
            int result = firstOperand * secondOperand;

            OperandToKeyStrokes(firstOperand);
            view.SimulateKeyPress("*");
            OperandToKeyStrokes(secondOperand);
            view.RequestResult();

            Assert.That(view.Result == result);
        }

        [Test]
        public void TestDivOneDigit()
        {
            int firstOperand = new Random().Next(0, 10);
            int secondOperand = new Random().Next(1, 10);
            float result = (float) firstOperand / secondOperand;

            view.SimulateKeyPress(firstOperand);
            view.SimulateKeyPress("/");
            view.SimulateKeyPress(secondOperand);
            view.RequestResult();

            Assert.That(view.Result == result);
        }

        [Test]
        public void TestDivTwoDigits()
        {
            int firstOperand = new Random().Next(10, 100);
            int secondOperand = new Random().Next(10, 100);
            float result = (float)firstOperand / secondOperand;

            OperandToKeyStrokes(firstOperand);
            view.SimulateKeyPress("/");
            OperandToKeyStrokes(secondOperand);
            view.RequestResult();

            Assert.That(view.Result == result);
        }

        [Test]
        public void TestDivThreeDigits()
        {
            int firstOperand = new Random().Next(100, 1000);
            int secondOperand = new Random().Next(100, 1000);
            float result = (float)firstOperand / secondOperand;

            OperandToKeyStrokes(firstOperand);
            view.SimulateKeyPress("/");
            OperandToKeyStrokes(secondOperand);
            view.RequestResult();

            Assert.That(view.Result == result);
        }

        [Test]
        public void TestAutoClearWhenTypingFirstOperandAfterResultComputed()
        {
            view.SimulateKeyPress(new Random().Next(1, 10));
            view.SimulateKeyPress("+");
            view.SimulateKeyPress(new Random().Next(1, 10));
            view.RequestResult();

            int newFirstOperand = new Random().Next(1, 10);
            view.SimulateKeyPress(newFirstOperand);

            Assert.That(view.FirstOperand == newFirstOperand);
            Assert.That(view.SecondOperand == 0);
            Assert.That(view.Operation == CalculatorOperation.Unknown);
            Assert.That(view.Result == 0f);
        }

        private void OperandToKeyStrokes(int value)
        {
            char[] operand = value.ToString().ToCharArray();
            for (var i = 0; i < operand.Length; i++)
            {
                view.SimulateKeyPress(operand[i].ToString());
            }
        }
    }
}
