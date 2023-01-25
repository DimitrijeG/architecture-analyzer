using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Core;
using Presenters;
using Readers;
using Analyzers;
using Xunit;

namespace Tests
{
    public class TestingCycles : AbstractTesting
    {
        [Fact, Trait("Category", "not")]
        public void CycleTest_exist_ToSelf()
        {
            ModelStructure modelStructure = GetModelStructure(rootPath+"CycleJson/exist_ToSelf/C1.json");

            AnalyzerOfCycles analyzer = new AnalyzerOfCycles(modelStructure);

            Assert.Equal(
                "C1", 
                analyzer.cycelsToString()
            );
        }

        [Fact, Trait("Category", "Running")]
        public void CycleTest_exist_Pair()
        {
            ModelStructure modelStructure = GetModelStructure(
                rootPath+"CycleJson/exist_Pair/C1.json",
                rootPath+"CycleJson/exist_Pair/C2.json"
            );

            AnalyzerOfCycles analyzer = new AnalyzerOfCycles(modelStructure);

            Assert.Equal(
                "C1, C2", 
                analyzer.cycelsToString()
            );
        }

        [Fact, Trait("Category", "not")]
        public void CycleTest_exist_OneTriangle()
        {
            ModelStructure modelStructure = GetModelStructure(
                rootPath+"CycleJson/exist_OneTriangle/C1.json",
                rootPath+"CycleJson/exist_OneTriangle/C2.json",
                rootPath+"CycleJson/exist_OneTriangle/C3.json"
            );

            AnalyzerOfCycles analyzer = new AnalyzerOfCycles(modelStructure);

            Assert.Equal(
                "C1, C2, C3", 
                analyzer.cycelsToString()
            );
        }

        [Fact, Trait("Category", "not")]
        public void CycleTest_notExist_OneTriangle()
        {
            ModelStructure modelStructure = GetModelStructure(
                rootPath+"CycleJson/notExist_OneTriangle/C1.json",
                rootPath+"CycleJson/notExist_OneTriangle/C2.json",
                rootPath+"CycleJson/notExist_OneTriangle/C3.json"
            );

            AnalyzerOfCycles analyzer = new AnalyzerOfCycles(modelStructure);

            Assert.Equal(
                "", 
                analyzer.cycelsToString()
            );
        }

        [Fact, Trait("Category", "not")]
        public void CycleTest_exist_RhomboidOneCycle()
        {
            ModelStructure modelStructure = GetModelStructure(
                rootPath+"CycleJson/exist_RhomboidOneCycle/C1.json",
                rootPath+"CycleJson/exist_RhomboidOneCycle/C2.json",
                rootPath+"CycleJson/exist_RhomboidOneCycle/C3.json",
                rootPath+"CycleJson/exist_RhomboidOneCycle/C4.json"

            );

            AnalyzerOfCycles analyzer = new AnalyzerOfCycles(modelStructure);

            Assert.Equal(
                "C2, C3, C4", 
                analyzer.cycelsToString()
            );
        }

        [Fact, Trait("Category", "not")]
        public void CycleTest_exist_RhomboidTwoCycles()
        {
            ModelStructure modelStructure = GetModelStructure(
                rootPath+"CycleJson/exist_RhomboidTwoCycles/C1.json",
                rootPath+"CycleJson/exist_RhomboidTwoCycles/C2.json",
                rootPath+"CycleJson/exist_RhomboidTwoCycles/C3.json",
                rootPath+"CycleJson/exist_RhomboidTwoCycles/C4.json"

            );

            AnalyzerOfCycles analyzer = new AnalyzerOfCycles(modelStructure);

            Assert.Equal(
                "C1, C2, C3\n" +
                "C2, C3, C4", 
                analyzer.cycelsToString()
            );
        }

        [Fact, Trait("Category", "not")]
        public void CycleTest_exist_TrapezoidTwoCycles()
        {
            ModelStructure modelStructure = GetModelStructure(
                rootPath+"CycleJson/exist_TrapezoidTwoCycles/C1.json",
                rootPath+"CycleJson/exist_TrapezoidTwoCycles/C2.json",
                rootPath+"CycleJson/exist_TrapezoidTwoCycles/C3.json",
                rootPath+"CycleJson/exist_TrapezoidTwoCycles/C4.json",
                rootPath+"CycleJson/exist_TrapezoidTwoCycles/C5.json"
            );

            AnalyzerOfCycles analyzer = new AnalyzerOfCycles(modelStructure);

            Assert.Equal(
                "C1, C2, C3, C4\n" +
                "C1, C2, C5", 
                analyzer.cycelsToString()
            );
        }
        
        [Fact, Trait("Category", "not")]
        public void CycleTest_exist_ComplexStructure1()
        {
            ModelStructure modelStructure = GetModelStructure(
                rootPath+"CycleJson/exist_ComplexStructure1/C1.json",
                rootPath+"CycleJson/exist_ComplexStructure1/C2.json",
                rootPath+"CycleJson/exist_ComplexStructure1/C3.json",
                rootPath+"CycleJson/exist_ComplexStructure1/C4.json",
                rootPath+"CycleJson/exist_ComplexStructure1/C5.json",
                rootPath+"CycleJson/exist_ComplexStructure1/C6.json",
                rootPath+"CycleJson/exist_ComplexStructure1/C7.json"
            );

            AnalyzerOfCycles analyzer = new AnalyzerOfCycles(modelStructure);

            Assert.Equal(
                "C1, C2, C6, C3, C4\n" +
                "C1, C2, C7, C3, C4", 
                analyzer.cycelsToString()
            );
        }

        [Fact, Trait("Category", "not")]
        public void CycleTest_exist_ComplexStructure2()
        {
            ModelStructure modelStructure = GetModelStructure(
                rootPath+"CycleJson/exist_ComplexStructure2/C1.json",
                rootPath+"CycleJson/exist_ComplexStructure2/C2.json",
                rootPath+"CycleJson/exist_ComplexStructure2/C3.json",
                rootPath+"CycleJson/exist_ComplexStructure2/C4.json",
                rootPath+"CycleJson/exist_ComplexStructure2/C5.json",
                rootPath+"CycleJson/exist_ComplexStructure2/C6.json",
                rootPath+"CycleJson/exist_ComplexStructure2/C7.json",
                rootPath+"CycleJson/exist_ComplexStructure2/C8.json",
                rootPath+"CycleJson/exist_ComplexStructure2/C9.json",
                rootPath+"CycleJson/exist_ComplexStructure2/C10.json"
            );

            AnalyzerOfCycles analyzer = new AnalyzerOfCycles(modelStructure);

            Assert.Equal(
                "C1, C4, C5, C6, C3, C2\n" +
                "C1, C4, C10, C5, C6, C3, C2\n" +
                "C4, C5, C6, C3, C2\n" +
                "C4, C10, C5, C6, C3, C2\n" +
                "C5, C7, C10\n" +
                "C8, C9", 
                analyzer.cycelsToString()
            );
        }
    }
}
