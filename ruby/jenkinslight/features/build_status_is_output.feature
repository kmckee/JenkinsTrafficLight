Feature: Build status is displayed
  As a user
  I want to see the current status of the build in the console
  So that I can tell if the application is working

  Scenario: Build succeeded and all tests pass
    Given I am monitoring a build
    When the last build succeeded and all tests passed
    Then I should see a message with the current time and "Build Status: Green"

  Scenario: Build succeeded with test failures
    Given I am monitoring a build
    When the last build succeeded but one or more tests failed
    Then I should see a message with the current time and "Build Status: Red\\tFailing tests"

  Scenario: Build failed
    Given I am monitoring a build
    When the last build failed and a build is not currently in progress
    Then I should see a message with the current time and "Build Status: Red\\tBuild failed"

  Scenario: Last build failed, new build in progress
    Given I am monitoring a build
    When a build is currently in process and the last build failed
    Then I should see a message with the current time and "Build Status: Yellow\\tBroken and building..."

  Scenario: Last build had test failures, new build in progress
    Given I am monitoring a build
    When a build is currently in process and the last build had test failures
    Then I should see a message with the current time and "Build Status: Yellow\\tTest failure\(s\) on previous build, rebuilding..."
  
  Scenario: Jenkins job is disabled
    Given I am monitoring a build
    When the jenkins job is disabled
    Then I should see a message with the current time and "Build Status: Unknown\\tJenkins is suspended"

  Scenario: Unable to connect to Jenkins
    Given I am monitoring a build
    When it's not possible to contact Jenkins for a status
    Then I should see a message with the current time and "Build Status: Unknown\\tError contacting Jenkins"
    
