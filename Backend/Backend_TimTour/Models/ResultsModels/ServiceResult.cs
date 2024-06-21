﻿namespace Backend_TimTour.Models.ResultsModels
{
    public enum ServiceResult
    {
        OK,
        PASSWORD_INVALID,
        PASSWORD_SUCCESSFULLY_ENCRYPTED,
        USER_CREATED,
        LOGIN_SUCCESFUL,
        USER_SUCCESFULLY_REGISTERED,
        REGISTRATION_FAILED,
        EMAIL_ALREADY_USED,
        USER_NOT_FOUND_IN_DATABASE,
        LOCATION_TYPE_SENT_IS_NOT_TREATED_IN_THE_DATABSE,
        LOCATION_NAME_CAN_NOT_BE_FOUND_IN_DATABASE,
        LOCATION_RATED_SUCCESFULLY,
        UNABLE_TO_UPDATE_LOCATION,
        LOCATION_IS_OPEN,
        LOCATION_IS_CLOSED,
        EMAIL_DOES_NOT_EXIST_IN_DB,
        REQUEST_TO_THE_RECOMMANDATION_SERVICE_FAILED
    }
}
