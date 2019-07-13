layoutModule.controller('jobSeekerRegisterController', ['$scope', 'dataService', '$window', 'toaster', 'Upload', 'convertorService', function ($scope, dataService, $window, toaster, Upload, convertorService) {

    $scope.getAll = function ({url, successFunc, messages}) {
        dataService.get(url).then(function (res) {

            console.log(res.data);
            if (res.data.Data.length > 0) {
                $scope.data = res.data.Data;
                $scope.totalItems = res.data.Count;
            }
            successFunc && successFunc();
            messages && messages();

        })

    }

    $scope.selectedCountry = "0";
    $scope.selectedState = "0";
    $scope.getAll({
        url: '/api/JobSeekerRegister/GetAllCountries/',
        successFunc: function () {
            $scope.countries = $scope.data;
            console.log('countries', $scope.countries)
        }
    });
    $scope.changeCountry = function () {
        $scope.getAll({
            url: '/api/JobSeekerRegister/GetStates/' + $scope.selectedCountry,
            successFunc: function () {

                $scope.states = $scope.data;
                console.log('state', $scope.states)


            }
        });


    }
    $scope.submit = function () {

     
       
       Upload.upload({
           url:'/api/JobSeekerRegister/Submit/',
           data:{
               FullName: $scope.fullName,
               Email: $scope.email,
               CountryId:$scope.selectedCountry,
               StateId:$scope.selectedState,
               City:$scope.city,
               JobTitle:$scope.jobTitle,
               Resume:$scope.resume,
               Password:$scope.password,    
           },
       }).then(function (res) {
           console.log(res);
       })

    }

    console.log('in job seeker registration');
}]);