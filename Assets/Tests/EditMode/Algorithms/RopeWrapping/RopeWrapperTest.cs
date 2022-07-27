using game.character.ability.rope;
using NUnit.Framework;
using UnityEngine;
using Utils.math;

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

            var neighbours = RopeWrapper.GetNeighbours(points, anchor, curr);
            
            Assert.AreEqual(points[2], neighbours.Value.Item1);
            Assert.AreEqual(points[1], neighbours.Value.Item2);
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

            var neighbours = RopeWrapper.GetNeighbours(points, anchor, curr);
            
            Assert.AreEqual(points[3], neighbours.Value.Item1);
            Assert.AreEqual(points[2], neighbours.Value.Item2);
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

        [Test]
        public void Updater()
        {
            var startPoint = new Vector2(0, 0);
            var points = new System.Collections.Generic.List<Vector2> {new Vector2(0, 4f)};

            var ropeWrapper = new RopeWrapper(startPoint, points);
            
            ropeWrapper.Update(new Vector2(1, 5));
            ropeWrapper.Update(new Vector2(-1, 5));
            Assert.AreEqual(1, ropeWrapper.GetSegments().Count);
            Assert.AreEqual(new Segment(startPoint, new Vector2(0, 4)), ropeWrapper.GetSegments()[0]);
        }
        
        [Test]
        public void Updater2()
        {
            var startPoint = new Vector2(2, 2);
            var points = new System.Collections.Generic.List<Vector2>
            {
                new Vector2(0, 0),
                new Vector2(0, 4),
                new Vector2( 4, 0),
                new Vector2(4, 4)
            };

            var ropeWrapper = new RopeWrapper(startPoint, points);
            
            ropeWrapper.Update(new Vector2(6, 2));
            ropeWrapper.Update(new Vector2(6, 4));
            ropeWrapper.Update(new Vector2(4, 6));
            ropeWrapper.Update(new Vector2(0, 6));
            ropeWrapper.Update(new Vector2(-1, 3));
            Assert.AreEqual(new System.Collections.Generic.List<Vector2>
            {
                new Vector2(2, 2),
                new Vector2(4, 4),
                new Vector2( 0, 4),
                new Vector2(-1, 3)
            }, ropeWrapper.GetPoints());
        }

        [Test]
        public void UpdaterWithOnlyStartAndCurrentPoint()
        {
            var startPoint = new Vector2(2, 2);
            var ropeWrapper = new RopeWrapper(startPoint, new());
            ropeWrapper.Update(new Vector2(4, 4));
            Assert.AreEqual(new System.Collections.Generic.List<Vector2>
            {
                new Vector2(2, 2),
                new Vector2(4, 4),
            }, ropeWrapper.GetPoints());
        }

        [Test]
        public void UpdaterWithBackTrack()
        {
            var startPoint = new Vector2(2, 2);
            var points = new System.Collections.Generic.List<Vector2>
            {
                new Vector2(0, 0),
                new Vector2(0, 4),
                new Vector2( 4, 0),
                new Vector2(4, 4)
            };

            var ropeWrapper = new RopeWrapper(startPoint, points);
            
            ropeWrapper.Update(new Vector2(6, 2));
            ropeWrapper.Update(new Vector2(6, 4));
            ropeWrapper.Update(new Vector2(4, 6));
            ropeWrapper.Update(new Vector2(0, 6));
            ropeWrapper.Update(new Vector2(-1, 3));
            ropeWrapper.Update(new Vector2(-1, 5));
            Assert.AreEqual(new System.Collections.Generic.List<Vector2>
            {
                new Vector2(2, 2),
                new Vector2(4, 4),
                new Vector2(-1, 5)
            }, ropeWrapper.GetPoints());
        }
        
        [Test]
        public void UpdaterWithBackTrackBackAndForth()
        {
            var startPoint = new Vector2(2, 2);
            var points = new System.Collections.Generic.List<Vector2>
            {
                new Vector2(4, 4)
            };
            var ropeWrapper = new RopeWrapper(startPoint, points);
            ropeWrapper.Update(new Vector2(5, 2));
            ropeWrapper.Update(new Vector2(2, 5));

            Assert.AreEqual(new System.Collections.Generic.List<Vector2>
            {
                new Vector2(2, 2),
                new Vector2(4, 4),
                new Vector2(2, 5),
            }, ropeWrapper.GetPoints());
            
            ropeWrapper.Update(new Vector2(5, 2));
            
            Assert.AreEqual(new System.Collections.Generic.List<Vector2>
            {
                new Vector2(2, 2),
                new Vector2(5, 2),
            }, ropeWrapper.GetPoints());
            
            ropeWrapper.Update(new Vector2(2, 5));
            
            Assert.AreEqual(new System.Collections.Generic.List<Vector2>
            {
                new Vector2(2, 2),
                new Vector2(4, 4),
                new Vector2(2, 5),
            }, ropeWrapper.GetPoints());
            
            ropeWrapper.Update(new Vector2(5, 2));
            
            Assert.AreEqual(new System.Collections.Generic.List<Vector2>
            {
                new Vector2(2, 2),
                new Vector2(5, 2),
            }, ropeWrapper.GetPoints());
        }
    }
}