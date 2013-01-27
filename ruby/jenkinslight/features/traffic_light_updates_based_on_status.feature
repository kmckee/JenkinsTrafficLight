Feature: Traffic light updates based on status
  As a team member
  I want to always know the current build status
  So that I can keep it green
  
  Scenario: Display green when the build is healthy 
    Given the build is currently Green
    When I look at the traffic light
    Then only the green light should be on

  Scenario: Display red for an unhealthy build
    Given the build is currently Red
    When I look at the traffic light
    Then only the red light should be on

  Scenario: Display yellow when the build is unhealthy, but rebuilding
    Given the previous build was unhealthy and a build is in progress
    When I look at the traffic light
    Then only the yellow light should be on

  Scenario: Turn off all lights if the build status is unknown
    Given the build status is unknown
    When I look at the traffic light
    Then all lights should be off
