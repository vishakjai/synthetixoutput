package com.example.userservice.controller

import com.example.userservice.dto.*
import com.example.userservice.service.UserService
import org.springframework.http.ResponseEntity
import org.springframework.web.bind.annotation.*

@RestController
@RequestMapping("/api")
class UserController(private val userService: UserService) {

    @PostMapping("/users")
    fun signup(@RequestBody request: SignupRequest): ResponseEntity<SignupResponse> {
        val response = userService.signup(request)
        return ResponseEntity.status(201).body(response)
    }

    @PostMapping("/users/login")
    fun login(@RequestBody request: LoginRequest): ResponseEntity<JwtAuthResponse> {
        val response = userService.login(request)
        return ResponseEntity.status(201).body(response)
    }

    @GetMapping("/user")
    fun getCurrentUser(): ResponseEntity<UserListResponse> {
        val response = userService.getCurrentUser()
        return ResponseEntity.ok(response)
    }

    @PutMapping("/user")
    fun updateUser(@RequestBody request: UpdateUserRequest): ResponseEntity<UpdateUserResponse> {
        val response = userService.updateUser(request)
        return ResponseEntity.ok(response)
    }

    @GetMapping("/profiles/{username}")
    fun getProfile(@PathVariable username: String): ResponseEntity<ProfileResponse> {
        val response = userService.getProfile(username)
        return ResponseEntity.ok(response)
    }

    @PostMapping("/profiles/{username}/follow")
    fun follow(@PathVariable username: String, @RequestBody request: FollowRequest): ResponseEntity<FollowResponse> {
        val response = userService.follow(username, request)
        return ResponseEntity.status(201).body(response)
    }

    @DeleteMapping("/profiles/{username}/follow")
    fun unfollow(@PathVariable username: String): ResponseEntity<UnfollowResponse> {
        val response = userService.unfollow(username)
        return ResponseEntity.ok(response)
    }
}