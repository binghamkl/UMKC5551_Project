
function ShowLoading(message) {
    $.mobile.loading('show', {
        text: message,
        textVisible: true,
        theme: 'b',
        html: ''
    });
}

function HideLoading() {
    $.mobile.loading('hide', {});
}



// Overall viewmodel for this screen, along with initial state
function AgileMindViewModel() {
    var self = this;

    // Non-editable catalog data - would come from the server

    // Editable data

    self.IsLoggedIn = ko.observable(false);
    self.Error = ko.observable("");

    self.LoginInfo = { UserName: ko.observable(""), Password: ko.observable("") };
    self.Register = { UserName: ko.observable(""), Password: ko.observable(""), Email: ko.observable("") };

    // Computed data

    // Operations

    //Login
    self.Login = function () {
        try {

            ShowLoading('Loading...');
            $('#loginPage').addClass('ui-disabled');

            $.ajax({
                type: 'POST',
                url: URIHOME + USER + 'LoginUser',
                data: { UserName: self.LoginInfo.UserName(), Password: self.LoginInfo.Password() },
                dataType: 'json',
                success: function (data) {
                    HideLoading();
                    $('#loginPage').removeClass('ui-disabled');

                    if (!data.Success) {
                        self.IsLoggedIn(false);
                        self.Error(data.Error);
                    }
                    else {
                        self.IsLoggedIn(true);
                        self.Error('');
                    }

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    HideLoading();
                    $('#loginPage').removeClass('ui-disabled');
                    self.IsLoggedIn(false);
                    self.Error(errorThrown);
                }

            });


        } catch (e) {
            HideLoading();
            $('#loginPage').removeClass('ui-disabled');
            self.Error(e.message);
            self.IsLoggedIn(false);
        }
    }


    //Login
    self.RegisterUser = function () {
        try {

            ShowLoading('Loading...');
            $('#CreateUser').addClass('ui-disabled');

            $.ajax({
                type: 'POST',
                url: URIHOME + USER + 'CreateUser',
                data: { UserName: self.Register.UserName(), Password: self.Register.Password(), Email: self.Register.Email() },
                dataType: 'json',
                success: function (data) {
                    HideLoading();
                    $('#CreateUser').removeClass('ui-disabled');

                    if (!data.Success) {
                        self.IsLoggedIn(false);
                        self.Error(data.Error);
                    }
                    else {
                        self.IsLoggedIn(true);
                        self.LoginInfo.UserName(self.Register.UserName());
                        self.LoginInfo.Password(self.Register.Password());
                        self.Error('');
                        $("#CreateUser").dialog("close");
                    }

                },
                error: function (jqXHR, textStatus, errorThrown) {
                    HideLoading();
                    $('#CreateUser').removeClass('ui-disabled');
                    self.IsLoggedIn(false);
                    self.Error(errorThrown);
                }

            });


        } catch (e) {
            HideLoading();
            $('#CreateUser').removeClass('ui-disabled');
            self.Error(e.message);
            self.IsLoggedIn(false);
        }
    }


}


