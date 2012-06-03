﻿using EditorUtils.Implementation.Outlining;
using Xunit;
using System;

namespace EditorUtils.UnitTest
{
    public sealed class AdhocOutlinerFactoryTest : EditorHost
    {
        private readonly AdhocOutlinerFactory _adhocOutlinerFactoryRaw;
        private readonly IAdhocOutlinerFactory _adhocOutlinerFactory;

        public AdhocOutlinerFactoryTest()
        {
            _adhocOutlinerFactoryRaw = new AdhocOutlinerFactory(TaggerFactory);
            _adhocOutlinerFactory = _adhocOutlinerFactoryRaw;
        }

        /// <summary>
        /// The GetOrCreate method should cache the provided value.  There should only be 
        /// one value per ITextBuffer
        /// </summary>
        [Fact]
        public void Get_CacheValue()
        {
            var textBuffer = CreateTextBuffer();
            var outliner1 = _adhocOutlinerFactory.GetAdhocOutliner(textBuffer);
            var outliner2 = _adhocOutlinerFactory.GetAdhocOutliner(textBuffer);
            Assert.Same(outliner1, outliner2);
        }

        /// <summary>
        /// Should produce a different IAdhocOutliner for every ITextBuffer
        /// </summary>
        [Fact]
        public void Get_DiffForEachTextBuffer()
        {
            var textBuffer1 = CreateTextBuffer();
            var textBuffer2 = CreateTextBuffer();
            var outliner1 = _adhocOutlinerFactory.GetAdhocOutliner(textBuffer1);
            var outliner2 = _adhocOutlinerFactory.GetAdhocOutliner(textBuffer2);
            Assert.NotSame(outliner1, outliner2);
        }

        /// <summary>
        /// Disposing the returned tagger shouldn't invalidate the IAdhocOutliner cached
        /// for the ITextBuffer.  It should appear to be an independent object
        /// </summary>
        [Fact]
        public void Dispose_KeepCache()
        {
            var textBuffer = CreateTextBuffer();
            var outliner1 = _adhocOutlinerFactory.GetAdhocOutliner(textBuffer);
            var tagger = _adhocOutlinerFactoryRaw.CreateTagger(textBuffer);
            ((IDisposable)tagger).Dispose();
            var outliner2 = _adhocOutlinerFactory.GetAdhocOutliner(textBuffer);
            Assert.Same(outliner1, outliner2);
        }
    }
}
