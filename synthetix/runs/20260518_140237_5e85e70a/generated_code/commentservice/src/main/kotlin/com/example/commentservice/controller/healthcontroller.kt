package com.example.commentservice.controller

import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.RestController

@RestController
class HealthController {

    @GetMapping("/health")
    fun health(): Map<String, String> = mapOf("status" to "healthy")

    @GetMapping("/ready")
    fun ready(): Map<String, String> = mapOf("status" to "ready")
}