@Homepage
Feature: Homepage
	Site functional testing

@Desktop
@Mobile
@HomepageFilterDropdownSelection
Scenario: Homepage Filter Dropdown Selection
	Given Homepage has launched
	When Select 'Speeches' from filter dropdown menu
	Then Validate page displayed 'Speeches'