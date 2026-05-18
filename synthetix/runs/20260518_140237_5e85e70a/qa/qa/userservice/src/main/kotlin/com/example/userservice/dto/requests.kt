package com.example.userservice.dto

data class SignupRequest(
    val username: String,
    val email: String,
    val password: String,
    val recaptcha_token: String
)

data class LoginRequest(
    val username: String,
    val password: String,
    val recaptcha_token: String
)

data class UpdateUserRequest(
    val customer_id: String,
    val account_no: String,
    val payload: String
)

data class FollowRequest(
    val customer_id: String,
    val account_no: String,
    val payload: String
)