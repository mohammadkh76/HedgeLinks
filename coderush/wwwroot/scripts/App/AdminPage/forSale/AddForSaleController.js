angModule.controller('addForSaleController', ['$scope', 'dataService', 'convertorService', 'Upload', '$window', 'toaster', function ($scope, dataService, convertorService, Upload, $window, toaster) {
    $scope.validate = true;
    $scope.beforeChange = function (files) {

        if (files.length > 5) {
            $scope.forsaleimage = [];

            toaster.pop('error', "File selection error", "the number of files could not be more than 5 files");
            $scope.validate = false;

        }
        let fileIndex = files.findIndex(x => x.size > 2097152);
        if (fileIndex >= 0) {
            toaster.pop('error', "File size error", "you cannot select file more than 2MB");
            $scope.validate = false;

        }

        // for (let i=0;i<files.length;i++){
        //     let fileSize=files[i].size /1024 /1024;
        //     if (fileSize>2)
        //     {
        //         toaster.pop('error', "File size error", "you cannot select file more than 2MB");
        //         $scope.forsaleimage=[];
        //
        //         validate=false;
        //     }
        // } 
    }
    $scope.changeUploadedPic = function () {
        if ($scope.validate) {

            return new Promise(function (resolve) {
                $scope.showLoading = true;

                let promiseList = [];
                if ($scope.data.File.length > 0) {
                    let files = $scope.data.File;
                    for (var i = 0; i < files.length; i++) {
                        promiseList.push(convertorService.toBase64(files[i]));

                    }

                    return Promise.all(promiseList).then(function (base64List) {
                        console.log(base64List);

                        $scope.rowCount = Math.ceil(base64List.length / 4);
                        $scope.forsaleimage = base64List;
                        $scope.showLoading = false;

                        $scope.$apply();
                        return resolve();

                    }).catch(function (err) {
                        console.trace(err);
                    })

                }

            })

        }
        else {
            $scope.validate = true;

        }


    }
    $scope.deleteUploadedPic = function (id) {
        $scope.forsaleimage.splice(id, 1);
        $scope.data.File.splice(id, 1);
    }
    $scope.forsaleimage = [];
    $scope.rowCount = 1;
    $scope.progressPercentage = 0;
    $scope.showLoading = false;
    $scope.submit = function (data) {
        dataService.upload({
            url: '/api/ForSaleApi/AddForSale/',

            data: {
                Title: data.Title,
                YearBuild: data.YearBuild,
                SquareFeetBid: data.SquareFeetBid,
                SquareFeetLand: data.SquareFeetLand,
                BedRooms: data.BedRooms,
                BathRoom: data.BathRooms,
                Location: data.Location,
                Garage: data.Garage,
                Description: data.Description,
                PriceArv: data.PriceArv,
                PriceNet: data.PriceNet,
                File: data.File
            },
            successCb(res) {
                $window.location.href = "/ForSales/Index";

            },
            progressCb(evt) {
                $scope.progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
                console.log('progress: ' + $scope.progressPercentage + '% ' + evt.config.data);
            }
        })
    };
}
]);