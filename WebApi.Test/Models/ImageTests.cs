using WebApi.Models;

namespace WebApi.Test.Models
{
    public class ImageTests
    {
        [Fact]
        public void ShouldAddOnlyOneObjectToHashSet()
        {
            var image1 = new Image { Id = 1, Filename = "abcd.jpg" };
            var image2 = new Image { Id = 1, Filename = "abcd234.jpg" };
            var image3 = new Image { Id = 1, Filename = "abcdfdwx.jpg" };

            var images = new HashSet<Image>
            {
                image1,
                image2,
                image3
            };

            var result = images.Count;
            var expectedResult = 1;

            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ShouldAddOnlyTwoObjectsToHashSet()
        {
            var image1 = new Image { Id = 1, Filename = "abcd.jpg" };
            var image2 = new Image { Id = 2, Filename = "abcd.jpg" };
            var image3 = new Image { Id = 2, Filename = "abcdfdwx.jpg" };

            var images = new HashSet<Image>
            {
                image1,
                image2,
                image3
            };

            var result = images.Count;
            var expectedResult = 2;

            Assert.Equal(expectedResult, result);
        }
    }
}
