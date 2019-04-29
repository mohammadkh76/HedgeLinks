module.controller("addVisaController", ["$scope", "vcRecaptchaService", "dataService", "Upload","RedirectToRoute", function ($scope, vcRecaptchaService, dataService, Upload,RedirectToRoute) {
    RedirectToRoute.pageState=0;
    $scope.steps = [
        {
            title: "Personal"
        },
        {
            title: "Passport"
        },
        {
            title: "Job"
        },
        {
            title: "document"
        },
        {
            title: "further"
        },

    ]
    $scope.data={};
    $scope.getCountries = function () {
        dataService.get('/api/VisaApi/GetCountry/').then(function (res) {
            $scope.country = res.data.country;

        }).catch(function (err) {

        })
    };
    $scope.getNationalities = function () {
        dataService.get('/api/VisaApi/GetNationality/').then(function (res) {
            $scope.nationality = res.data.nationality;

        }).catch(function (err) {

        })
    };
    $scope.data = {};
    $scope.current = 1;
    $scope.disabledSubmit=false;
    $scope.data.SelectedGender = '0';
    $scope.data.SelectedMartial = '0';
    $scope.data.SelectedCountry = '1';
    $scope.data.SelectedNationality = '1';
    $scope.getCountries();
    $scope.getNationalities();
    $scope.nextStep = function (current) {
        $scope.current = current + 1;
    }
    $scope.prevStep = function (prev) {
        $scope.current = prev - 1;
    }
    $scope.setWidgetId = function (widgetId) {
        $scope.widgetId = widgetId;
    }

    $scope.cbExpiration = function () {
        $scope.recaptchaResponse = null;

    }
    $scope.setResponse = function (response) {
        $scope.disabledSubmit=true;
        Upload.upload({
            url: '/api/VisaApi/AddVisa/',
            data: {
                BirthDate: $scope.data.BirthDate,
                CityBirth: $scope.data.CityBirth,
                Comment: $scope.data.Comment,
                DateOfIssue: $scope.data.DateOfIssue,
                DateOfPlace: $scope.data.DateOfPlace,
                DepartureDate: $scope.data.DepartureDate,
                EmbassyName: $scope.data.EmbassyName,
                Email: $scope.data.Email,
                EntranceDate: $scope.data.EntranceDate,
                ExpirationDate: $scope.data.ExpirationDate,
                FatherName: $scope.data.FatherName,
                GrandFatherName: $scope.data.GrandFatherName,
                SpouseName: $scope.data.SpouseName,
                NationalityId: $scope.data.SelectedNationality,
                CountryId: $scope.data.SelectedCountry,
                FieldOfActivity: $scope.data.FieldOfActivity,
                HomeAddress: $scope.data.HomeAddress,
                Name: $scope.data.Name,
                Occupation: $scope.data.Occupation,
                PassportFile: $scope.data.PassportFile,
                PassportNumber: $scope.data.PassportNumber,
                PassportType: $scope.data.PassportType,
                PhoneNumber: $scope.data.PhoneNumber,
                Picture: $scope.data.PictureFile,
                Position: $scope.data.Position,
                HowLongStay: $scope.data.HowLongStay,
                Gender: $scope.data.SelectedGender,
                Martial: $scope.data.SelectedMartial,
                VisitDate: $scope.data.VisitDate,
                WorkplaceName: $scope.data.WorkplaceName,
                WorkplaceAddress: $scope.data.WorkplaceAddress,
                RecaptchaRes: response
            }

        }).then(function (res) {
            toastr.success(res.data.message);
            $scope.disabledSubmit=false;

            $scope.data={};
        }).catch(function (err) {
            toastr.error(err.data.message);
            $scope.disabledSubmit=true;
        });
        // dataService.post('/api/VisaApi/AddVisa/', $scope.formData).then(function (res) {
        //     console.log(res);
        // }).catch(function (err) {
        //     console.log(err);
        // })
        //
    }
    
    $scope.submitClick = function () {
        $scope.isValidate = true;
        if (!$scope.data.HowLongStay) {
            toastr.error('How Long Stay is required');
            $scope.isValidate = false;

        }
        if (!$scope.data.EmbassyName) {
            toastr.error('Embassy Name is required');
            $scope.isValidate = false;

        }
        if (!$scope.data.EntranceDate) {
            toastr.error('Entrance Date is required');
            $scope.isValidate = false;

        }
        if (!$scope.data.DepartureDate) {
            toastr.error('Departure Date is required');
            $scope.isValidate = false;

        }
        if (!$scope.data.PhoneNumber) {
            toastr.error('Phone Number is required');
            $scope.isValidate = false;
        }

        if (!$scope.data.HomeAddress) {
            toastr.error('Home Address Date is required');
            $scope.isValidate = false;
        }
        if ($scope.isValidate) {
            vcRecaptchaService.execute($scope.widgetId);

        }


    }
    $scope.validateSection1 = function (secOneData) {
        RedirectToRoute.pageState=1;


        $scope.isValidate = true;
        if (!secOneData.Name) {
            toastr.error('name is required')
            $scope.isValidate = false;

        }
        if (!secOneData.BirthDate) {
            toastr.error('Birth Date is required')
            $scope.isValidate = false;

        }
        if (!secOneData.CityBirth) {
            toastr.error('City Birth is required')
            $scope.isValidate = false;

        }
        if (!secOneData.FatherName) {
            toastr.error('Father Name is required')
            $scope.isValidate = false;

        }
        if (!secOneData.Email) {
            toastr.error('Email is required')
            $scope.isValidate = false;
        }
        if ($scope.isValidate) {
            $scope.nextStep($scope.current);

            window.location.href = '#/section2';
        }

    }
    $scope.validateSection2 = function (secTwoData) {
        RedirectToRoute.pageState=1;


        $scope.isValidate = true;
        if (!secTwoData.PassportNumber) {
            toastr.error('Passport Number is required');
            $scope.isValidate = false;

        }
        if (!secTwoData.PassportType) {
            toastr.error('Passport Type is required')
            $scope.isValidate = false;

        }
        if (!secTwoData.DateOfIssue) {
            toastr.error('Date of Issue is required')
            $scope.isValidate = false;

        }
        if (!secTwoData.DateOfPlace) {
            toastr.error('Date Of Place is required')
            $scope.isValidate = false;

        }
        if (!secTwoData.ExpirationDate) {
            toastr.error('Expiration Date is required')
            $scope.isValidate = false;
        }
        if ($scope.isValidate) {
            $scope.nextStep($scope.current);

            window.location.href = '#/section3';
        }


    }
    $scope.validateSection3 = function (secThreeData) {
        RedirectToRoute.pageState=1;


        $scope.isValidate = true;
        if (!secThreeData.Occupation) {
            toastr.error('Occupation Number is required');
            $scope.isValidate = false;

        }
        if (!secThreeData.WorkplaceName) {
            toastr.error('Workplace Name is required');
            $scope.isValidate = false;

        }
        if (!secThreeData.WorkplaceAddress) {
            toastr.error('Workplace Address is required');
            $scope.isValidate = false;

        }
        if (!secThreeData.Position) {
            toastr.error('Position is required');
            $scope.isValidate = false;

        }
        if (!secThreeData.FieldOfActivity) {
            toastr.error('Field Of Activity Date is required');
            $scope.isValidate = false;
        }

        if ($scope.isValidate) {
            $scope.nextStep($scope.current);

            window.location.href = '#/section4';
        }


    }


}])
