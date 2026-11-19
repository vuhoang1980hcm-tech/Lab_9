using BasicMath;

namespace BasicMathTests
{
    [TestClass]
    public class BasicMathsTests
    {
        private readonly BasicMaths bm = new BasicMaths();

        [TestMethod]
        [DataRow(1, 1, 2)]                                     // EP: positive
        [DataRow(-1, -1, -2)]                                  // EP: negative
        [DataRow(0, 0, 0)]                                     // EP: zero
        [DataRow(int.MaxValue, 1, (double)int.MaxValue + 1)]   // BVA: upper
        [DataRow(int.MinValue, -1, (double)int.MinValue - 1)]  // BVA: lower
        public void Test_AddMV(double a, double b, double expected)
            => Assert.AreEqual(expected, bm.Add(a, b));

        [TestMethod]
        [DataRow(5, 3, 2)]
        [DataRow(-5, -3, -2)]
        [DataRow(0, 0, 0)]
        [DataRow(int.MinValue, 1, (double)int.MinValue - 1)]   // BVA
        public void Test_SubtractMV(double a, double b, double expected)
            => Assert.AreEqual(expected, bm.Subtract(a, b));

        [TestMethod]
        [DataRow(2, 3, 6)]
        [DataRow(-2, -3, 6)]
        [DataRow(0, 5, 0)]
        [DataRow(int.MaxValue, 1, (double)int.MaxValue)]       // BVA
        public void Test_MultiplyMV(double a, double b, double expected)
            => Assert.AreEqual(expected, bm.Multiply(a, b));

        [TestMethod]
        [DataRow(6, 3, 2)]
        [DataRow(-6, -3, 2)]
        [DataRow(0, 1, 0)]
        [DataRow(int.MaxValue, 1, (double)int.MaxValue)]       // BVA
        public void Test_DivideMV(double a, double b, double expected)
            => Assert.AreEqual(expected, bm.Divide(a, b));

        [TestMethod]
        public void Test_DivideByZero_Throws()
            => Assert.ThrowsExactly<DivideByZeroException>(() => bm.Divide(1, 0));
    }
}