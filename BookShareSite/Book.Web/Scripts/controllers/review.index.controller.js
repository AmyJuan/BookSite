app.controller('ReivewIndexController', ReivewIndexController)
ReivewIndexController.$inject = ['$scope', 'ReviewService', 'UtilService'];
function ReivewIndexController($scope, ReviewService, UtilService) {
    var id = UtilService.getRequest()['id'];
    $scope.addResult = "";
    GetReviewsInAPI(id);

    function GetReviewsInAPI(bookid) {
        ReviewService.getReviews(bookid)
        window.searchReview = function (data) {
            var users = [];
            var reviews = [];
            console.log("review is:  {0}", data)
            angular.forEach(data.annotations, function (item) {
                var user = item.author_user;
                var u = {
                    ID: user.id,
                    Uid: user.uid,
                    Name: user.name,
                    Password: '',
                    Alt: user.alt,
                    Image: user.avatar,
                    Create: !user.created ? 0 : Date.parse(user.created)
                }
                users.push(u);

                var r = {
                    ID: item.id,
                    BookId: item.book_id,
                    UserId: item.author_id,
                    Content: item.content,
                    Updated: !item.time ? 0 : Date.parse(item.time)
                }
                reviews.push(r);
            })
            console.log("users is:  {0}, reviews is {1}", users, reviews);
            SaveReviewsAndUsers(users, reviews);
        }
    }

    function SaveReviewsAndUsers(users, reviews) {
        ReviewService.apiSave({ Users: users, Reviews: reviews })
            .success(function (result) {
                $scope.addResult = "Successful"
                //$scope.user.Name = result;
                //console.log("save collections {0}", result)
            })
           .error(function (e) {
               $scope.addResult = "Error"
               console.log("save collections error {0}", e)
           })
    }
}