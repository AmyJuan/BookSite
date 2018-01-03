app.directive('pager', function () {
    return {
        restrict: 'E',
        templateUrl: '/Template/Pager.html',
        replace: true,
        scope: {
            previous: '&',
            next: '&',
            total: '=',
            index: '='
        },
        //controller: function ($scope) {
        //    $scope.previous() = function () {
        //        if ($scope.index > 1) {
        //            $scope.index--;
        //        }
        //    }

        //    $scope.next() = function () {
        //        if ($scope.index < $scope.total) {
        //            $scope.index++;
        //        }
        //    }

        //}
    }
});