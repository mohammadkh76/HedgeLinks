angModule.controller('homeController', ['$scope', 'dataService', 'parallaxService', function ($scope, dataService, parallaxService) {
    console.log('in the home controller');
    dataService.get('/api/TopImageApi/GetTopImage').then(function (res) {
        $scope.topimage = res.data.topimage[0];
    });

    //
    dataService.get('/api/MiddleImageApi/GetMiddleImage').then(function (res) {
        $scope.middleimage = res.data.middleimage[0];
        $('#middleParallax').parallax({imageSrc: $scope.middleimage.ImagePath});
    });
    dataService.get('/api/BottomTileApi/GetBottomTile').then(function (res) {
        $scope.bottomTiles = res.data.bottomtile;
        console.log($scope.bottomTiles)
    });
    dataService.get('/api/BottomSliderApi/GetBottomSlider').then(function (res) {
        $scope.bottomSlider = res.data.bottomslider;
        
        console.log($scope.bottomSlider)
    });
    dataService.get('/api/SecondMiddlePageApi/GetSecondMiddleImage').then(function (res) {
        $scope.secondMiddleImage = res.data.secondMiddleImage[0];
        $('#parallax2').parallax({imageSrc: $scope.secondMiddleImage.ImagePath});

        console.log($scope.secondMiddleImage)
    });




    //
    // dataService.get('/api/HomeApi/GetBottomImage').then(function (res) {
    //     $scope.about=res.data.about;
    // });
    //
    // dataService.get('/api/HomeApi/GetBottomTile').then(function (res) {
    //     $scope.about=res.data.about;
    // });
    //
    // dataService.get('/api/HomeApi/GetBottomSlider').then(function (res) {
    //     $scope.about=res.data.about;
    // })
}]);