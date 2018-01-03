app.controller('UserMarksController', UserReviewsController)
UserReviewsController.$inject = ['$scope', 'UserService', 'UtilService'];
function UserReviewsController($scope, UserService, UtilService) {
    var id = UtilService.getRequest()['id'];
    init()

    function init() {
        UserService.load(id)
            .success(function (result) {
                $scope.user = result;
                $scope.user.CreateStr = new Date(result.Create);
            })
           .error(function (e) {
               console.log("Save Books And Collections error {0}", e)
           })
    }
}