using System;
using Algorithms.RopeWrapping;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditMode.Algorithms.RopeWrapping
{
    public class RopeWrapperTest
    {
        [Test]
        public void GetNeighboursFromOrigin()
        {
            var anchor = new Vector2(0f, 0f);
            var curr = new Vector2(-5f, 0f);
            var points = new System.Collections.Generic.List<Vector2>
            {
                new Vector2(0f, -5f),
                new Vector2(-5f, -1f),
                new Vector2(-5f, 2f),
                new Vector2(0f, 5f),
                new Vector2(5f, 0f)
            };

            var (leftNeighbour, rightNeighbour) = RopeWrapper.GetNeighbours(points, anchor, curr);
            
            Assert.AreEqual(points[2], leftNeighbour);
            Assert.AreEqual(points[1], rightNeighbour);
        }
        
        [Test]
        public void GetNeighboursFromTranslatedOrigin()
        {
            var anchor = new Vector2(2f, 3f);
            var curr = new Vector2(5f, 1f);
            var points = new System.Collections.Generic.List<Vector2>
            {
                new Vector2(-2f, -2f),
                new Vector2(-1f, 4f),
                new Vector2(4f, 4f),
                new Vector2(4f, -3f)
            };

            var (leftNeighbour, rightNeighbour) = RopeWrapper.GetNeighbours(points, anchor, curr);
            
            Assert.AreEqual(points[3], leftNeighbour);
            Assert.AreEqual(points[2], rightNeighbour);
        }

        [Test]
        public void HasSideChangedTrue()
        {

            var anchorPoint = new Vector2(0f, 5f);
            var targetPoint = new Vector2(0f, 0f);
            var currPoint = new Vector2(0.1f, 0f);
            var prevCurrPoint = new Vector2(-0.1f, 0f);

            var hasSideChanged = RopeWrapper.HasSideChanged(targetPoint, currPoint, prevCurrPoint, anchorPoint);

            Assert.AreEqual(true, hasSideChanged);
        }

        [Test]
        public void HasSideChangedFalse()
        {

            var anchorPoint = new Vector2(0f, 5f);
            var targetPoint = new Vector2(0f, 0f);
            var currPoint = new Vector2(0.1f, 0f);
            var prevCurrPoint = new Vector2(0.2f, 0f);

            var hasSideChanged = RopeWrapper.HasSideChanged(targetPoint, currPoint, prevCurrPoint, anchorPoint);

            Assert.AreEqual(false, hasSideChanged);
        }
        
        [Test]
        public void HasSideChangedTrueDiagonalAnchorLine()
        {

            var anchorPoint = new Vector2(2f, 2f);
            var targetPoint = new Vector2(4f, 4f);
            var currPoint = new Vector2(3.5f, 4.5f);
            var prevCurrPoint = new Vector2(4.5f, 3.5f);

            var hasSideChanged = RopeWrapper.HasSideChanged(targetPoint, currPoint, prevCurrPoint, anchorPoint);

            Assert.AreEqual(true, hasSideChanged);
        }
        
        [Test]
        public void HasSideChangedFalseDiagonalAnchorLine()
        {

            var anchorPoint = new Vector2(2f, 2f);
            var targetPoint = new Vector2(4f, 4f);
            var currPoint = new Vector2(4.5f, 3f);
            var prevCurrPoint = new Vector2(4.5f, 3.5f);

            var hasSideChanged = RopeWrapper.HasSideChanged(targetPoint, currPoint, prevCurrPoint, anchorPoint);

            Assert.AreEqual(false, hasSideChanged);
        }
    }
}