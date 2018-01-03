app.controller('HomeIndexController', UserIndexController)
UserIndexController.$inject = ['$scope', 'UserService', 'UtilService'];
function UserIndexController($scope, UserService, UtilService) {
    var id = UtilService.getRequest()['id'];
    $scope.addResult = "";
    init()

    $scope.getBooks = function () {
        GetBooksInAPI(id);
    }

    function init() {
        var options = {
            //Boolean - If we show the scale above the chart data    
            // 网格线是否在数据线的上面        
            scaleOverlay: false,

            //Boolean - If we want to override with a hard coded scale
            // 是否用硬编码重写y轴网格线
            scaleOverride: false,

            //** Required if scaleOverride is true **
            //Number - The number of steps in a hard coded scale
            // y轴刻度的个数
            scaleSteps: null,
            //Number - The value jump in the hard coded scale
            // y轴每个刻度的宽度
            scaleStepWidth: null,
            //Number - The scale starting value
            // y轴的起始值
            scaleStartValue: null,

            //String - Colour of the scale line    
            // x轴y轴的颜色
            scaleLineColor: "rgba(0,0,0,1)",

            //Number - Pixel width of the scale line
            // x轴y轴的线宽    
            scaleLineWidth: 1,

            //Boolean - Whether to show labels on the scale    
            // 是否显示y轴的标签
            scaleShowLabels: true,

            //Interpolated JS string - can access value
            // 标签显示值
            scaleLabel: "<%=value%>",

            //String - Scale label font declaration for the scale label
            // 标签的字体
            scaleFontFamily: "'Arial'",

            //Number - Scale label font size in pixels    
            // 标签字体的大小
            scaleFontSize: 12,

            //String - Scale label font weight style
            // 标签字体的样式
            scaleFontStyle: "normal",

            //String - Scale label font colour    
            // 标签字体的颜色
            scaleFontColor: "#666",

            ///Boolean - Whether grid lines are shown across the chart
            // 是否显示网格线
            scaleShowGridLines: true,

            //String - Colour of the grid lines
            // 网格线的颜色
            scaleGridLineColor: "rgba(0,0,0,.1)",

            //Number - Width of the grid lines
            // 网格线的线宽
            scaleGridLineWidth: 1,

            //Boolean - Whether the line is curved between points
            // 是否是曲线
            bezierCurve: true,

            //Boolean - Whether to show a dot for each point
            // 是否显示点
            pointDot: true,

            //Number - Radius of each point dot in pixels
            // 点的半径
            pointDotRadius: 3,

            //Number - Pixel width of point dot stroke
            // 点的线宽
            pointDotStrokeWidth: 1,

            //Boolean - Whether to show a stroke for datasets
            datasetStroke: true,

            //Number - Pixel width of dataset stroke
            // 数据线的线宽
            datasetStrokeWidth: 3,

            //Boolean - Whether to fill the dataset with a colour
            // 数据线是否填充颜色
            datasetFill: true,

            ////Boolean - Whether to animate the chart
            //// 是否有动画效果
            //animation: true,

            //Number - Number of animation steps
            // 动画的步数
            animationSteps: 60,

            //String - Animation easing effect
            // 动画的效果
            animationEasing: "easeOutQuart",

            //Function - Fires when the animation is complete
            // 动画完成后调用
            onAnimationComplete: null
        }
        var data = {
            labels: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
            datasets: [
                {
                    fillColor: "rgba(220,220,220,0.5)",
                    strokeColor: "rgba(220,220,220,1)",
                    pointColor: "rgba(220,220,220,1)",
                    pointStrokeColor: "#fff",
                    data: [6, 5, 9, 8, 5, 5, 4, 1, 1, 2, 3, 5]
                },
                {
                    fillColor: "rgba(151,187,205,0.5)",
                    strokeColor: "rgba(151,187,205,1)",
                    pointColor: "rgba(151,187,205,1)",
                    pointStrokeColor: "#fff",
                    data: [2, 4, 4, 1, 9, 2, 10, 2, 4, 5, 2, 5]
                }
            ]
        }
        var myLine = new Chart($("#canvas").get(0).getContext("2d"), { type: 'line', data: data, options: options });
        UserService.load(id)
            .success(function (result) {
                $scope.user = result;
                $scope.user.CreateStr = new Date(result.Create);
            })
           .error(function (e) {
               console.log("Save Books And Collections error {0}", e)
           })
    }

    function GetBooksInAPI(userId) {
        UserService.getBooks(userId)
        window.searchCollections = function (data) {
            console.log("get Collections is:  {0}, {1}", userId, data)
            var books = [];
            var collections = [];
            angular.forEach(data.collections, function (col) {
                var book = col.book;
                var b = {
                    ID: book.id,
                    Title: book.title,
                    OriginTitle: book.origin_title,
                    Summary: book.summary,
                    Image: book.image,
                    Authors: book.author,
                    Translators: book.translator
                }
                books.push(book);
                var c = {
                    ID: col.id,
                    BookId: col.book_id,
                    UserId: col.user_id,
                    Status: col.status,
                    Updated: !col.updated ? 0 : Date.parse(col.updated)
                }
                collections.push(c);
            })
            console.log("users is:  {0}, reviews is {1}", books, collections);
            SaveBooksAndCollections(books, collections);
        }
    }

    function SaveBooksAndCollections(books, collections) {
        UserService.apiSave({ Books: books, Collections: collections })
            .success(function (result) {
                $scope.addResult = "Successful"
                console.log("successful")
                //$scope.user.Name = result;
                //console.log("save collections {0}", result)
            })
           .error(function (e) {
               $scope.addResult = "Error"
               console.log("Save Books And Collections error {0}", e)
           })
    }

}