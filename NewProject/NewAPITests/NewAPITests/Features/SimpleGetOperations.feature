Feature: SimpleGetOperations
	

Scenario: Simple Get Operation by using ID
	Given I perform Get operation on "api/unknown/{GetID}"
	When I pass valid GetID
	Then I should see the response details


