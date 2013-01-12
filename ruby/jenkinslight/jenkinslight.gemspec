# -*- encoding: utf-8 -*-
lib = File.expand_path('../lib', __FILE__)
$LOAD_PATH.unshift(lib) unless $LOAD_PATH.include?(lib)
require 'jenkinslight/version'

Gem::Specification.new do |gem|
  gem.name          = "jenkinslight"
  gem.version       = Jenkinslight::VERSION
  gem.authors       = ["Kyle McKee"]
  gem.email         = ["mckee.kyle@gmail.com"]
  gem.description   = %q{This gem can be used with a teensy microcontroller to connect a traffic light to a jenkins CI server. }
  gem.summary       = %q{Used to connect a teensy to a Jenkins CI server.}
  gem.homepage      = "https://github.com/kmckee/JenkinsTrafficLight"

  gem.files         = `git ls-files`.split($/)
  gem.executables   = gem.files.grep(%r{^bin/}).map{ |f| File.basename(f) }
  gem.test_files    = gem.files.grep(%r{^(test|spec|features)/})
  gem.require_paths = ["lib"]
  
  gem.add_development_dependency 'rake'
end
